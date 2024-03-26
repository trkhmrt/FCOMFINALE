using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Requests.LogRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    public class LogController : Controller
    {
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string cookieValue);

            var accessToken = cookieValue;

            LogRequest logRequest = new LogRequest();

            var logs = await logRequest.GetAllLogs(accessToken);

             if(logs.Item2.Success)
             {
                return View(logs.Item1.ToList());
            }


            return View();
        }
    }
}

