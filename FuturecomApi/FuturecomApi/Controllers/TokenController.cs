﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EfRepositories;
using DataAccessLayer.TokenManager;
using DtoLayer.TokenDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomApi.Controllers
{
    [Route("/[controller]")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
    
        UserLogManager logManager = new UserLogManager(new EfUserLogRepo());

        public TokenController(UserManager<User> userManager)
        {
            _userManager = userManager;
            
        }

        [HttpPost("checktoken")]
        public async Task<IActionResult> CheckToken()
         {




            var tokenHandler = new JwtSecurityTokenHandler();

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator();

            var tokenValidator = new TokenValidator();


            var securityToken = tokenHandler.ReadJwtToken(token);

            var claims = securityToken.Claims;

            var userIdClaim = claims.FirstOrDefault(c => c.Type == "id").Value.ToString();

          

            var isValidAccess = tokenValidator.ValidateToken(token);

                if(isValidAccess)
                {
                    var myUser = await _userManager.FindByIdAsync(userIdClaim);

                    var role = await _userManager.GetRolesAsync(myUser);

                    var newAccesstoken = accessTokenGenerator.CreateToken(myUser, role.ToList());

                logManager.TInsert("RT",userIdClaim);

                return Ok(newAccesstoken);
        
                }
                else
                {
                return Unauthorized("Token Geçerli değil");
                }
              
            

              
        } 


        
    }
}

