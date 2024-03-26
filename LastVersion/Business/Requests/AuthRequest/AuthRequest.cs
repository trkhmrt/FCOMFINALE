using System;
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

namespace BusinessLayer.Requests
{
    public class AuthRequest
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        Context context = new Context();
       
        public AuthRequest(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
          
        }
      

        public async Task<ApiResponse> AuthApiRequest(UserLoginDto model)
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
                    var role = claims.FirstOrDefault(c => c.Type == "role")?.Value;
                    var firstname = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    var lastname = claims.FirstOrDefault(c => c.Type == "lastname")?.Value;           
                    var phonenumber = claims.FirstOrDefault(c => c.Type == "phone")?.Value;

                 

                    var result = context.TokenUsers.FirstOrDefault(u => u.Id == userId);

                    if(result==null)
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
                    if (role != "Admin")
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

                        var addToRole = await _userManager.AddToRoleAsync(user, role);
                    }


                    var founduser = await _userManager.FindByNameAsync(model.Username);

                    founduser.SecurityStamp = Guid.NewGuid().ToString();

                    var signIn = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                 

                    return new ApiResponse { Success = true, Message = "Giriş Başarılı" ,refreshToken= refreshToken , accessToken = accessToken };
                
                   

                }
                else
                {
                   
                    var error = await response.Content.ReadAsStringAsync();
                  


                    return new ApiResponse { Success = false, Message = error };
                }
                }
            }

        }
    }



