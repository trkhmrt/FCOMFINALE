using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.EfRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomApi.Controllers
{
    [Route("/[controller]")]
 
    public class LogController : Controller
    {

        UserLogManager logManager = new UserLogManager(new EfUserLogRepo());

      
        [HttpGet("getlogs")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetLogs()
        {

           var logs = logManager.TGetList();
            if(logs!=null)
            {
                return Ok(logs);
            }

            return BadRequest("Not Listing");
        }

      
    }
}

