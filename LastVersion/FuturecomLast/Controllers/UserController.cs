using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Business.Requests;
using Business.Requests.UserRequest;
using BusinessLayer.Validator;
using DataAccessLayer.Concrete;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FuturecomLast.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {


        UserUpdateByName userUpdateByName = new UserUpdateByName();
        RegisterRequest registerRequest = new RegisterRequest();
        UserGetByIdRequest userGetByIdRequest = new UserGetByIdRequest();
        UserListRequest userListRequest = new UserListRequest();
        UserDeleteRequest userDeleteRequest = new UserDeleteRequest();
        PasswordValidator passwordValidator = new PasswordValidator();
        UserStatusChangeRequest usc = new UserStatusChangeRequest();


        private readonly UserManager<User> _userManager;

      

        public UserController(UserManager<User> userManager)
        {
             _userManager = userManager;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> UserAdd()
        {
          
            return View();
           
            
        }


       [HttpPost]
        public async Task<IActionResult> UserAdd(UserRegisterDto model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Request.Cookies.TryGetValue("accessToken", out string cookieValue);

                var accessToken = cookieValue;

                var result = await registerRequest.AddUser(model, accessToken);

                if (result.Success)
                {

                    ViewBag.message = result.Message;

                    return RedirectToAction("UserListAll");

                
                }

                ModelState.AddModelError("", result.Message);

            }



            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UserDelete(string id)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string cookieValue);

            var accessToken = cookieValue;

            await userDeleteRequest.DeleteUser(id,accessToken);

          
            return RedirectToAction("UserListAll");
           





        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserUpdateNormalUser()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserListAll(string id)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string cookieValue);

            var accessToken = cookieValue;


            var content = await userListRequest.GetAllUsers(accessToken);

            if (content.Item2.Success)
            {
                return View(content.Item1.ToList());
            }
            else
            {
                ViewBag.error = content.Item2.Message;
            }
            
            
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> UserUpdate(string  id)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string cookieValue);

            var accessToken = cookieValue;

            var user =  await userGetByIdRequest.GetUserById(id, accessToken);

            if (user != null)
            {
                return View(user);
            }
            
            return RedirectToAction("UserListAll");
            
        }

     
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserUpdateDto user)
        {

            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessToken);

            var result = await userUpdateByName.UserUpdate(user, accessToken);

            if (result.Success)
            {
                return RedirectToAction("UserUpdate");
            }

              return RedirectToAction("UserListAll");

        }
     



        [Authorize(Roles ="NormalUser,Admin")]
        public async Task<IActionResult> UserChangePw(UserPwUpdateDto info)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessToken);

            HttpContext.Request.Cookies.TryGetValue("username", out string username);




            if (Request.Method == "POST")
            {
                var result = passwordValidator.Validate(info);
               if (result.IsValid)
                {
                    ChangePwRequest changePwRequest = new ChangePwRequest();

                    info.username = username;

                   var content = await changePwRequest.ChangePw(info,accessToken);

                    if (!content.Success)
                    {
                        ViewBag.error = content.Message;
                    }
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
                return View();





            }
            else if (Request.Method == "GET")
             {
                    return View();
             }
            return View();
            }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UserStatusUpdate(string id,bool status)
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out string accessToken);

            UserStatusUpdateDto userstatus = new UserStatusUpdateDto();

            userstatus.id = id;
            userstatus.status = status;

            var result = await usc.UserUpdate(userstatus, accessToken);

            if (result.Success)
            {
                return RedirectToAction("UserListAll");
            }
            else
            {

                return RedirectToAction("UserListAll");
            }
           
        }


    }
}

