using System;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EfRepositories;
using DataAccessLayer.TokenManager;
using DtoLayer.LoginDto;
using DtoLayer.UserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FuturecomApi.Middlewares;

namespace FuturecomApi.Controllers
{
    [Route("/[controller]")]
    [AllowAnonymous]
    
    public class AuthController : Controller
    {
        
        private readonly UserManager<User> _userManager;
        

        AccessTokenGenerator tokenGenerator = new AccessTokenGenerator();
        RefreshTokenManager refreshTokenManager = new RefreshTokenManager();
        UserLogManager logManager = new UserLogManager(new EfUserLogRepo());


        public AuthController(UserManager<User> userManager)
        {
            
            _userManager = userManager;

        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

 
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized("Oturum açma başarısız");
            }
            else
            {

                var role = await _userManager.GetRolesAsync(user);
                var token = tokenGenerator.CreateToken(user, role.ToList());
                var refreshtoken = refreshTokenManager.CreateRefreshToken(user);

                logManager.TInsert("SI", $"{user.Id}");

                return Ok(new { AccessToken = token, RefreshToken = refreshtoken });

            }

         
                
        }



        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] UserLogoutDto logoutDto)
        {

            var user = await _userManager.FindByIdAsync(logoutDto.id);

            var userId = await _userManager.GetUserIdAsync(user);

            logManager.TInsert("LO", $"{userId}");

            return Ok();
            
           
        }
    }

 }



