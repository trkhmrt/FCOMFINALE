using System;
using EntityLayer.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccessLayer.TokenManager
{
	public class RefreshTokenManager
	{
        public string CreateRefreshToken(User user)
        {




            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetfuturecom_project_strong_jwt_key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimDizisi = new[]
           {
                new Claim("id",user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7069/",
                audience: "https://localhost:7069/",
                claims:claimDizisi,
                expires: DateTime.UtcNow.AddDays(10), 
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);




        }
    }
}

