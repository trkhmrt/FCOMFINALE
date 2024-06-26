﻿using System;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Azure;
using Business.Services;
using DataAccessLayer.Concrete;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Components.Routing;


namespace BusinessLayer.Requests
{
    public class AuthRequest
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        Context context = new Context();

        public AuthRequest(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;


        }


        public async Task<ApiResponse> AuthApiRequest(UserLoginDto model)
        {


            try
            {
                var jsonData = JsonConvert.SerializeObject(model);
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri("https://localhost:7069/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var response = await client.PostAsync("auth/login", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                  

                   if (response.IsSuccessStatusCode)
                    {

                        var content = await response.Content.ReadAsStringAsync();
                        var jsonObject = JObject.Parse(content);


                        var accessToken = jsonObject["accessToken"]?.ToString();
                        var refreshToken = jsonObject["refreshToken"]?.ToString();

                      


                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(accessToken);
                        var claims = token.Claims;

                        // Kullanıcı bilgilerini claims'lerden ayıkladım.
                        var userId = claims.FirstOrDefault(c => c.Type == "id")?.Value;
                        var username = claims.FirstOrDefault(c => c.Type == "username")?.Value;
                        var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;

                        List<Role> roles = new List<Role>();
                        foreach (var item in claims)
                        {
                            if (item.Type == "role")
                            {
                                Role role = new Role
                                {
                                    Id = Guid.NewGuid(),
                                    Name = item.Value,
                                    NormalizedName = item.Value.ToUpper()
                                };



                                roles.Add(role);
                            }
                        }
                        var roleNames = roles.Select(r => r.Name).ToList();


                        var firstname = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                        var lastname = claims.FirstOrDefault(c => c.Type == "lastname")?.Value;
                        var phonenumber = claims.FirstOrDefault(c => c.Type == "phone")?.Value;



                        var result = context.TokenUsers.FirstOrDefault(u => u.Id == userId);

                        if (result == null)
                        {
                            TokenUser tokenUser = new TokenUser
                            {
                                Id = userId,
                                AccessToken = accessToken,
                                RefreshToken = refreshToken,
                                UserName = username,
                            };
                            await context.TokenUsers.AddAsync(tokenUser);
                            await context.SaveChangesAsync();
                        }



                        var founduser = await _userManager.FindByNameAsync(model.Username);
                        if (founduser != null)
                        {
                            var roller = await _userManager.GetRolesAsync(founduser);


                            await _userManager.UpdateSecurityStampAsync(founduser);

                            await _userManager.RemoveFromRolesAsync(founduser, roller);

                            

                            await _userManager.AddToRolesAsync(founduser, roleNames);




                            await _userManager.RemovePasswordAsync(founduser);

                            await _userManager.AddPasswordAsync(founduser, "Abc_123456");




                            var signIn = await _signInManager.PasswordSignInAsync(model.Username, "Abc_123456", false, false);

                        }
                        else
                        {

                            User user = new User
                            {
                                UserName = username,
                                Id = Guid.NewGuid(),
                                FirstName = firstname,
                                LastName = lastname,
                                Email = email,
                                SecurityStamp = Guid.NewGuid().ToString()

                            };


                            var createUser = await _userManager.CreateAsync(user, "Abc_123456");

                            user.SecurityStamp = Guid.NewGuid().ToString();



                            var signIn = await _signInManager.PasswordSignInAsync(model.Username, "Abc_123456", false, false);

                        };

                           


                        
                       

                      

                   
                        
                        return new ApiResponse { Success = true, Message = "Giriş Başarılı", refreshToken = refreshToken, accessToken = accessToken };




                    }
                    else
                    {
                        if(response.StatusCode == HttpStatusCode.NotFound)
                        {

                            return new ApiResponse { Success = false, Message = "Api adresini kontrol ediniz." };
                        }
                        else if(response.StatusCode == HttpStatusCode.Unauthorized)
                        {

                            return new ApiResponse { Success = false, Message = "Kullanıcı adı veya parola Hatalı" };
                        }
                        else
                        {
                            return new ApiResponse { Success = false, Message = response.ReasonPhrase };
                        }
                    

                        
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                return new ApiResponse { Success = false, Message = "Oturum açma esnasında bir hata meydana geldi" };
            }
        }

    }
}

        
    



