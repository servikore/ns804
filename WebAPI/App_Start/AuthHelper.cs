using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.App_Start
{
    public class AuthHelper
    {
        public string GenerateTokenJwt(User user)
        {                
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConstants.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenHeader = new JwtHeader(signingCredentials);
                
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
                
            var tokenPayload = new JwtPayload(
                    issuer: JWTConstants.Issuer,
                    audience: JWTConstants.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,                        
                    expires: DateTime.UtcNow.AddDays(1)
                );

            var token = new JwtSecurityToken(
                    tokenHeader,
                    tokenPayload
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}