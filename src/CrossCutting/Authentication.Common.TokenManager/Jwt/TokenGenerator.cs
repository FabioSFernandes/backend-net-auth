using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenMan.Jwt
{
    public class TokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AppAuthSettings _authSettings;

        public TokenGenerator(IOptions<JwtSettings> jwtSettings, IOptions<AppAuthSettings> authSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _authSettings = authSettings.Value;
        }

        public string GenerateJwtToken(bool isAuthenticated, bool isAnonymous, string userId = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            if (!isAnonymous)
            {
                if (isAuthenticated) // App token requesting user token
                {
                    // User is authentication 
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
                }
                else
                {
                    // User is not authenticated
                    throw new Exception("Not authorized");
                }
                // Adicione outros claims conforme necessário
            }
            else
            {
                if (isAuthenticated) // App token requesting registration token 
                {
                    // User is anonymous
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, "allow-user-registration-limited-token"));
                }
                else
                {
                    throw new Exception("Not authorized");
                }
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateTemporaryToken(TokenRequestDto requestDto)
        {
            // Aqui você pode adicionar lógica para validar ProductId, AppId e AppSecret
            // Por exemplo, verificar se eles correspondem a um produto e aplicativo registrados
            if (requestDto.ProductId != _authSettings.ProductId || requestDto.AppId != _authSettings.AppId || requestDto.AppSecret != _authSettings.AppSecret)
            {
                //throw HttpClientBuilderExtensions.BadRequest("Access Denied");
                throw new Exception("Access Denied");
            }

            // Implementação simplificada da geração do token
            var claims = new List<Claim>
            {
                new Claim("ProductId", requestDto.ProductId),
                new Claim("AppId", requestDto.AppId)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expira em 30 minutos
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
