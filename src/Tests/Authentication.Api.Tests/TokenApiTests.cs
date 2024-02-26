using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Data;
using Xunit.Abstractions;
using Microsoft.Extensions.Options;

namespace Authentication.Api.Tests
{
    public class TokenApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;

        public TokenApiTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
            
            _factory.Server.BaseAddress = new Uri("https://localhost:5001");
            _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //services.AddTransient<ICtxUsers, CtxUsers>();
                    //services.AddTransient<ICtxTokens, CtxTokens>();
                });
            });
            
        }

        [Fact]
        public void Test2()
        {

        }

        
        //[Fact]
        [Theory]
        [InlineData("/token/request-token")]
        public async Task TokenEndpoint_ReturnsSuccessAndToken_WithValidCredentials(string url)
        {
            var _client = _factory.CreateClient();
            var parameters = new
            {
                productId = "x",
                appId = "y",
                appSecret = "z"
            };
            var json = JsonSerializer.Serialize(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var token = await response.Content.ReadAsStringAsync();
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.True(response.StatusCode.As<int>()<299, "Token should not be served with wrong auth attempt");
        }

        [Theory]
        [InlineData("/token/anonymous-token")]
        public async Task TokenEndpoint_AnonymousToken(string url)
        {
            var _client = _factory.CreateClient();
            var parameters = new
            {
                productId = "12345",
                appId = "12345",
                appSecret = "12345"
            };
            var json = JsonSerializer.Serialize(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var token = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.True(response.StatusCode.As<int>() < 299, "Token should not be served with wrong auth attempt");
        }

        [Theory]
        [InlineData("/")]
        public async Task TokenEndpoint_ReturnsHello(string url)
        {
            var _client = _factory.CreateClient();
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/health")]
        public async Task TokenEndpoint_ReturnsHealth(string url)
        {
            var _client = _factory.CreateClient();
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        //[Fact]
        [Theory]
        [InlineData("/token/login")]
        public async Task LoginEndpoint_ReturnsSuccess(string url)
        {
            var _client = _factory.CreateClient();
            var tokenParameters = new
            {
                productId = "12345",
                appId = "12345",
                appSecret = "12345"
            };
            var json = JsonSerializer.Serialize(tokenParameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var tokenResponse = await _client.PostAsync("/token/request-token", content);
            var token = await tokenResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<JsonElement>(token, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            string tokenData = responseData.GetProperty("token").GetString();
            tokenResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.False(string.IsNullOrEmpty(token), "Response Json should not be null or empty");
            Assert.True(tokenData.Length>0, "Token data should not be null or empty");

            var loginParameters = new
            {
                userId = "12345",
                password = "12345",
            };


            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData);
            json = JsonSerializer.Serialize(loginParameters);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            var loginResponse = await _client.PostAsync(url, content);
            var loginToken = await loginResponse.Content.ReadAsStringAsync();
            loginResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.False(string.IsNullOrEmpty(token), "Token should not be null or empty");

        }

        [Theory]
        [InlineData("/weatherforecast")]
        public async Task TokenEndpoint_ReturnsWeatherForecast(string url)
        {
            var _client = _factory.CreateClient();
            Assert.True(url.Length >= 0, $"URL OK: {url}");
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var token = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(token), "Token should not be null or empty");
        }

        private void log(string message)
        {
            if (_output!=null) _output.WriteLine(message);
        }
    }
}