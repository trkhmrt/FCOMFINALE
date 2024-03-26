using System;
using DataAccessLayer.Concrete;
using DtoLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FuturecomLast.ViewComponents
{
	public class UserSideMenu:ViewComponent
	{
        public IViewComponentResult Invoke()
        {
            

        


            return View();
        }
    }
}

