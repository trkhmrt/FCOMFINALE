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

             //var identity = new ClaimsIdentity(claims, "Bearer");

            // Create a new ClaimsPrincipal using the user's identity
             //var principal = new ClaimsPrincipal(identity);



            return View();
        }

    }
}

