using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using TokenMan.Jwt;
using TokenMan.Serializeable;

namespace TokenMan.Extensions
{
    public static class ASPNetHelper
    {
        static public void configureJTW(this WebApplicationBuilder builder)
        {
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

        }
    }
}
