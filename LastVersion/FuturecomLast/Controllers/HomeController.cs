using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
      

        [HttpGet]
        public IActionResult Index()
        {


            return View();      
        }


        [HttpGet]
        public IActionResult User()
        {


            return View();
        }

    }
}

