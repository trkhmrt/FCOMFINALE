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
        public async Task<IActionResult> ReToken()
        {
            
          /*       

            RefreshTokenRequest reToken = new RefreshTokenRequest();

           // var result = await reToken.CheckToken();

            if (result.Success)
            {
                return RedirectToAction();
            }
            */

            return View();
        }
    }
}

