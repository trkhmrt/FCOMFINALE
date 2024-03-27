using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Requests.UserRequest;
using DtoLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    public class UserProfileController : Controller
    {
        UserProfileUpdate userUpdateByName = new UserProfileUpdate();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(NormalUserUpdateDto user)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessToken);
            HttpContext.Request.Cookies.TryGetValue("username", out string username);

            ViewBag.username = username;

        
            userUpdateByName.UserUpdate(user, accessToken);


            return View();
        }
    }
}

