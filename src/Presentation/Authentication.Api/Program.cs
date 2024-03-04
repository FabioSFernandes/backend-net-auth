using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using TokenMan.Extensions;
using TokenMan.Jwt;
using Infrastructure.Data.Contexts;
using TokenMan.Serializeable;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddTransient(_ => new CtxUsers(builder.Configuration["AppConfig:LoginRegistryDB"]));
builder.Services.AddTransient(_ => new CtxTokens(builder.Configuration["AppConfig:LoginRegistryDB"]));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(item: new JsonStringEnumConverter());
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    // Outras configurações...
});

builder.configureJTW();

var app = builder.Build();

app.MapGet("/oi", () => {

    string nome = "Henry";
    string apelidos = "Detona Half, goido";
    DateTime dataNascimento = new DateTime(2022, 5, 28);
    string habilidadeMaior = "Força e inteligência";
    string poder = "leite";
    string fraqueza = "Adora bolacha wafer de chocolate Bauducco";
    int idade = 20; // meses
    float peso = 16.55f;
    double altura = 0.85;

    string texto = $"O {nome} e um goidim muito forte." +
                   $"Ele nasceu em {dataNascimento}, portanto tem {idade} meses.";


    return ("Oi voce me chamou pela internet");


});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{

    app.UseHttpsRedirection();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();





var tokenApi = app.MapGroup("/token");




tokenApi.MapPost("/login", [Authorize] ([FromServices]TokenGenerator tokenService, [FromBody]LoginDto loginDto, HttpContext httpContext) =>
{
    // login only can be called from a valid app token 
    bool? isValidUser = httpContext.User.Identity?.IsAuthenticated; // Simulação

    if (isValidUser ?? false)
    {
        // Generated a L1 authenticated token for the user
        var accessToken = tokenService.GenerateJwtToken(true, false, loginDto.UserId); // Implemente este método
        LoginResponseDto responseDto = new LoginResponseDto()
        {
            UserName = loginDto.UserId,
            perfil = new List<string>() { "admin", "user" },
            Token = accessToken
        };
        return Results.Ok(responseDto);
    }

    return Results.Unauthorized();
});

tokenApi.MapPost("/request-token", ([FromServices]TokenGenerator tokenService, [FromBody]TokenRequestDto requestDto) =>
{
    try
    {
        // Generates a L0 authentication token. Without this caller cannot have access to the Login neither registration API
        var token = tokenService.GenerateTemporaryToken(requestDto);
        return Results.Ok(new TokenResponseDto { Token = token });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

tokenApi.MapGet("/anonymous-token", [Authorize] ([FromServices]TokenGenerator tokenService, [FromBody]TokenRequestDto appData, HttpContext httpContext) =>
{
    // login only can be called from a valid app token 
    bool? isValidUser = httpContext.User.Identity?.IsAuthenticated;

    if (isValidUser ?? false)
    {
        // Generated a L1 authenticated token for the user, this token serves only for registration purposes
        var accessToken = tokenService.GenerateJwtToken(true, true);
        return Results.Ok(new TokenResponseDto() { Token = accessToken });
    }

    return Results.Unauthorized();
});

app.MapGet("/health", () =>
{
    return Results.Ok(Results.Empty);
});

app.MapGet("/", () =>
{
    return Results.Ok("Hello!");
});






app.Run();

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public partial class Program { }