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
       
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ReToken()
        {
            RefreshTokenRequest refreshToken = new RefreshTokenRequest();

            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessvalue);
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string refreshvalue);

            var reToken = refreshvalue;

            var result = await refreshToken.CheckToken(reToken);

            if(result!=null)
            {


                return RedirectToAction("Index", "Home");
            }

            


            return View();
        }
    }
}

