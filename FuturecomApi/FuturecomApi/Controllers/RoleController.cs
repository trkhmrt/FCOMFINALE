using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;
using DtoLayer.RoleDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomApi.Controllers
{
    [Route("/[controller]")]
    [Authorize]
    public class RoleController : Controller
    {
        Context context = new Context();
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }




        // GET api/values/5
        [HttpGet("addrole/{rolename}")]
        public async Task<IActionResult> AddRole(string rolename)
        {
            if (rolename != null)
            {
                Role role = new Role();
                role.Name = rolename;
                role.NormalizedName = rolename.ToUpper();
                role.Id = Guid.NewGuid();

                context.Roles.Add(role);
                context.SaveChanges();
                return Ok();
            }

            return StatusCode(400, "Role name is empty");

        }

        // POST api/values
        [HttpGet("{userid}")]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(id));
            var roles = _roleManager.Roles.ToList();





            return Ok();
        }



        [HttpGet]
        [Route("getallroles")]
        public async Task<IActionResult> GetAllRoles()
        {

            var roles =  _roleManager.Roles.ToList();

            return Ok(roles);


        }



        [HttpGet("getuserrole/{userId}")]

        public async Task<IActionResult> GetUserRole(string userId)
        {
            
            var user = _userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                foreach (var item in userRoles)
                {
                    Console.WriteLine(item);
                }
                return Ok(userRoles);
            }
            else
            {
                return NotFound("User Not found");
            }
            

       

        }


        [HttpPost]
        [Route("updaterole")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleAssignModel model)
        {
            
            var user = await _userManager.FindByIdAsync(model.userId);

            var roller = await _userManager.GetRolesAsync(user);

            List<Role> newRoles = new List<Role>();

            foreach (var item in model.Roles)
            {
                Role role = new Role
                {
                    Id = item.RoleID,
                    Name = item.Name,
                    NormalizedName = item.Name.ToUpper()
                };

                newRoles.Add(role);
            }

            var roleNames = newRoles.Select(r => r.Name).ToList();

            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roller);
            await _userManager.AddToRolesAsync(user, roleNames);


            return Ok();


           

       

          
  
        }

    }
}

