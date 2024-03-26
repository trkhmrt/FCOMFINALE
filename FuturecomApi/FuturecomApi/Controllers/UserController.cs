using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Abstract.IGenericUserServices;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EfRepositories;
using DataAccessLayer.TokenManager;
using DtoLayer.UserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace FuturecomApi.Controllers
{


    
    [Route("/[controller]")]
  
    public class UserController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly RoleManager<Role> _roleManager;
        UserLogManager logManager = new UserLogManager(new EfUserLogRepo());
        Context context = new Context();
       

        


        public UserController(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
         
        }


    


        [HttpGet("listuser")]
        [Authorize]
        public async Task<IActionResult> ListUser()
        {


            var users = context.Users.ToList();


            return Ok(users);
        }



        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }



        //OK
        [HttpPost("adduser")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterDto user)
        {

            User newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName,

            };

            var createResult = await _userManager.CreateAsync(newUser, user.Password);

            if (createResult.Succeeded)
            {
                var roleAssign = await _userManager.AddToRoleAsync(newUser, "NormalUser");

                if (roleAssign.Succeeded)
                {



                    logManager.TInsert("AU",$"{newUser.Id}");

                    return Ok("User add!");
                }
            }
            



                return BadRequest("There is problem!");                   

        }




        [HttpPost]
        [Route("changepw")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordDto request)
        {
            
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    return BadRequest("Invalid user");
                }


                if (!await _userManager.CheckPasswordAsync(user, request.CurrentPw))
                {
                    return BadRequest("Incorrect current password");
                }


                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPw, request.NewPw);
                if (result.Succeeded)
                {


                    logManager.TInsert("CP", $"{user.Id}");

                    return Ok("Password changed successfully");


                }

                return BadRequest("Failed to change password");
            
           
        }




        [HttpPut]
        [Route("adminupdate")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {

            var user = await _userManager.FindByNameAsync(userUpdateDto.UserName);

            if (user == null)
            {
                return NotFound();
            }



            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName; 
            user.Email = userUpdateDto.Email;
            user.PhoneNumber = userUpdateDto.PhoneNumber;


            
           var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                logManager.TInsert("UU", $"{user.Id}");
                return Ok();
            }
            else
            {
                return StatusCode(500, "Error updating user");
            }

       
            }

        [HttpPut]
        [Route("normaluser/{userId}")]
        public async Task<IActionResult> NormalUserUpdate(string userId, [FromBody] UserUpdateNormalDto userN)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }



         
            user.Email = userN.mail;
            user.PhoneNumber = userN.phone;



            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
               // logManager.LogAdd(userId, "IU");
                return Ok();
            }
            else
            {
                return StatusCode(500, "Error updating user");
            }


        }



        [HttpPut("status/{id}")]
        public async Task<IActionResult> StatusUpdate(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

           
            user.Status = !user.Status;

            var result = await _userManager.UpdateAsync(user);

            return Ok(); 

           
           
        }

      


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            
                var foundUser = await _userManager.FindByIdAsync(userId);

                if(foundUser!=null)
                {
                    await _userManager.DeleteAsync(foundUser);

                    logManager.TInsert("UD",$"{foundUser.Id}");

                    return Ok();

                }

            return BadRequest("Delete Failed");





        }














    }


}







