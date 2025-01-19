using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helper
{
    public static class JwtTokenHelper
    {
        public static string GenerateToken(Domain.Entities.User user, IConfiguration configuration)
        {
            // Retrieve JWT settings from configuration
            var jwtSettings = configuration.GetSection("JwtSettings");

            // Get and validate the secret key
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
            if (key.Length < 32) // Minimum 256 bits (32 characters)
                throw new ArgumentOutOfRangeException(nameof(key), "The secret key must be at least 256 bits (32 characters) long.");

            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiration = int.Parse(jwtSettings["ExpirationInMinutes"]);

            // Create claims for the token
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim("IsPlayer", user.IsPlayer.ToString())
        };

            // Generate signing credentials
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
