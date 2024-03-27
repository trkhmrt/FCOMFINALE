using System;
using System.Security.Claims;
using DataAccessLayer.Concrete;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FuturecomLast.ViewComponents
{
	public class UserView : ViewComponent
	{
        


        public IViewComponentResult Invoke()
        {
            HttpContext.Request.Cookies.TryGetValue("username", out string username);

            ViewBag.username = username;



            return View();
        }

    }
}

