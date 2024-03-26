using System;
using EntityLayer.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace DataAccessLayer.TokenManager
{
	public class AccessTokenGenerator
	{
        

        public string CreateToken(User user, string role)
        {




            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetfuturecom_project_strong_jwt_key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimDizisi = new[]
            {
                new Claim("id",user.Id.ToString()),             
                new Claim("role",role),
                new Claim("name",user.FirstName),
                new Claim("lastname",user.LastName),
                new Claim("role",role),
                new Claim("email",user.Email),
                new Claim("phone",user.PhoneNumber),
                new Claim("username",user.UserName)


            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7069/", 
                audience: "https://localhost:7069/",
                claims:claimDizisi,

                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                
                
                );

            return new JwtSecurityTokenHandler().WriteToken(token);




        }
    }
}

