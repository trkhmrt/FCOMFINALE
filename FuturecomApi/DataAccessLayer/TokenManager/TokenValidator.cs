using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace DataAccessLayer.TokenManager
{
	public class TokenValidator
	{
        public bool  ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return (false); 
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetfuturecom_project_strong_jwt_key")), 
                ValidateIssuer = true, 
                ValidIssuer = "https://localhost:7069/",
                ValidateAudience = true, 
                ValidAudience = "https://localhost:7069/",
                ValidateLifetime = true, 
                
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);






                
                return (true);

            }
            catch (SecurityTokenException ex)
            {

              
                return (false);
            }
        }
    }
}

