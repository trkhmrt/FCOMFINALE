using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Requests;
using Business.Requests.RoleRequest;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using FuturecomLast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuturecomLast.Controllers
{
    public class RoleController : Controller
    {

        RoleGetUser userRole = new RoleGetUser();
        UserGetByIdRequest getUser = new UserGetByIdRequest();
        RoleAddRequest roleAddRequest = new RoleAddRequest();
        RoleGetAll roles = new RoleGetAll();

        public IActionResult AddRole()
        {
            


                return View();

           
        }
        [HttpPost]
        public IActionResult AddRole(string rolename)
        {
            RoleAddRequest roleAddRequest = new RoleAddRequest();
            if (rolename != null)
            {
                if (roleAddRequest.AddRole(rolename).Result)
                {
                    return RedirectToAction("AddRole");
                }
               
                
            }


            return RedirectToAction("AddRole");


        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            try
            {

            var accessToken = HttpContext.Session.GetString("accessToken");
            var allRoles = await roles.GetAllRoles();
            var user = await getUser.GetUserById(id.ToString(),accessToken);
            var userRoles = await userRole.GetUserRole(id.ToString());

            RoleUpdateRequestDto roleUpdateRequestDto = new RoleUpdateRequestDto();

            roleUpdateRequestDto.userId = id;

            foreach (var item in allRoles)
            {
                RoleUpdateDto rolemodel = new RoleUpdateDto();
                rolemodel.UserId = Guid.Parse(id);
                rolemodel.RoleID = item.Id;
                rolemodel.Name = item.Name;
                rolemodel.Exists = userRoles.Contains(item.Name);
                roleUpdateRequestDto.Roles.Add(rolemodel);
            }

               
                return View(roleUpdateRequestDto);

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

          

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleUpdateRequestDto model)
        {
          
            try
            {

               
               RoleUpdateRequestDto updateRoleUpdateRequestDto = new RoleUpdateRequestDto();




                updateRoleUpdateRequestDto.userId = model.userId;
                foreach (var item in model.Roles)
                {
                    if(item.Exists)
                    {
                        RoleUpdateDto rolemodel = new RoleUpdateDto();
                        rolemodel.UserId = item.UserId;
                        rolemodel.RoleID = item.RoleID;
                        rolemodel.Name = item.Name;
                        rolemodel.Exists = true;
                        updateRoleUpdateRequestDto.Roles.Add(rolemodel);
                    }                                                       
                }

                RoleUpdateRequest roleUpdateRequest = new RoleUpdateRequest();


                roleUpdateRequest.AddRoleWithList(updateRoleUpdateRequestDto);
              

                return RedirectToAction("AssignRole", "Role");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("Index", "Home");

        }


    }
}

