using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Business.Services;
using BusinessLayer.Requests;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using FuturecomLast.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    [AllowAnonymous]
   
    public class AuthController : Controller
    {
       
       private  UserManager<User> _userManager;
       private  SignInManager<User> _signInManager;
       private  IUserService _userService;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,IUserService userService)
        {

           _userManager = userManager;
           _signInManager = signInManager;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //var result = _userService.GetUserName();

           

            if (User.Identity.IsAuthenticated)
            {
                
              

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            AuthRequest authRequest = new AuthRequest(_userManager, _signInManager);

            if (ModelState.IsValid)
            {
                
                var result = await authRequest.AuthApiRequest(model);
                
                if (!result.Success)
                {



                    ViewBag.Hata = result.Message;
                    return View();
              
                }


           
                HttpContext.Response.Cookies.Append("accessToken", result.accessToken, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(3), 
                    IsEssential = true, 
                    HttpOnly = true 
                });


           
                HttpContext.Response.Cookies.Append("refreshToken", result.refreshToken, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(3), 
                    IsEssential = true,
                    HttpOnly = true 
                });

                HttpContext.Response.Cookies.Append("username",model.Username);


                return RedirectToAction("Index", "Home");



            }
            return View();

        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return RedirectToAction("Login");
        }
        }
    }



