using Application.DTOs;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TokenApi.Utils;

var builder = WebApplication.CreateSlimBuilder(args);

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

builder.Services.AddSingleton(_ => new CtxUsers(builder.Configuration["AppConfig:LoginRegistryDB"]));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);
builder.Services.AddSingleton<TokenGenerator>();

var appAuth = builder.Configuration.GetSection("AppAuthSettings");
builder.Services.Configure<AppAuthSettings>(builder.Configuration.GetSection("AppAuthSettings"));
//builder.Services.AddSingleton<TokenGenerator>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Get<JwtSettings>().ValidIssuer,
            ValidAudience = jwtSettings.Get<JwtSettings>().ValidAudience,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Get<JwtSettings>().IssuerSigningKey))
        };
    });


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// app.UseAuthentication();
//app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

var tokenApi = app.MapGroup("/token");




app.MapPost("/login", [Authorize] (TokenGenerator tokenService, LoginDto loginDto, HttpContext httpContext) =>
{
    // Aqui você pode verificar as credenciais do usuário
    // E também pode acessar o token JWT e suas claims se necessário
    // Exemplo: var productId = httpContext.User.FindFirst("ProductId")?.Value;

    // Implementação da lógica de login
    bool isValidUser = true; // Simulação

    if (isValidUser)
    {
        // Gerar um token de acesso (diferente do token temporário)
        var accessToken = tokenService.GenerateJwtToken(true, loginDto.Username); // Implemente este método
        return Results.Ok(new TokenResponseDto() { Token  = accessToken });
    }

    return Results.Unauthorized();
});

/*app.MapPost("/loginOLD", async (TokenGenerator tokenService, LoginDto loginDto) =>
{
    // Substitua esta parte pelo seu método de validação de usuário
    bool isValidUser = true; // Simulação

    if (isValidUser)
    {
        var token = tokenService.GenerateJwtToken(true, "IdDoUsuario"); // Substitua pelo seu método de geração de token
        return Results.Ok(new { token });
    }

    return Results.Unauthorized();
});
*/
tokenApi.MapPost("/request-token", (TokenGenerator tokenService, TokenRequestDto requestDto) =>
{
    try
    {
        var token = tokenService.GenerateTemporaryToken(requestDto);
        return Results.Ok(new TokenResponseDto { Token = token });
    }
        catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }   
});

tokenApi.MapGet("/anonymous-token", (TokenGenerator tokenService, TokenRequestDto appData) =>
{
    if (appData.AppSecret != "123" || appData.AppId != "123" || appData.ProductId != "123")
    {
        return Results.Unauthorized();
    }
    var token = tokenService.GenerateJwtToken(false); // Substitua pelo seu método de geração de token
    return Results.Ok(new { token });
});


app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(TokenDto[]))]
[JsonSerializable(typeof(TokenRequestDto[]))]
[JsonSerializable(typeof(TokenResponseDto[]))]
[JsonSerializable(typeof(LoginDto[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
