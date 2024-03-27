using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Requests.TokenRequest;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    public class RefreshTokenController : Controller
    {
       
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ReToken()
        {
            RefreshTokenRequest refreshToken = new RefreshTokenRequest();

            
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string refreshvalue);

            var reToken = refreshvalue;

            var result = await refreshToken.CheckToken(reToken);

            if(result.Item1.Success)
            {
                Response.Cookies.Delete("accessToken");

                HttpContext.Response.Cookies.Append("accessToken", result.Item2, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(3),
                    IsEssential = true,
                    HttpOnly = true
                });


                return RedirectToAction("Index", "Home");
            }

            


            return View();
        }
    }
}

