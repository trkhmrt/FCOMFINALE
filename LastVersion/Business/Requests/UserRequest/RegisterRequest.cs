using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Requests
{
    public class RegisterRequest
    {


        public async Task<ApiResponse> AddUser(UserRegisterDto model, string token)
        {
            


                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await httpClient.PostAsync("https://localhost:7069/user/adduser", content);





                    if (response.IsSuccessStatusCode)
                    {
                        
                        var icerik = await response.Content.ReadAsStringAsync();
                        



                        return new ApiResponse { Success = true, Message = "Giriş Başarılı" };


                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {

                    return new ApiResponse { Success = false, Message = "TOKEN GEÇERLİ DEĞİL TEKRARDAN GİRİŞ YAPIN YADA TOKEN YENİLEYİN" };
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {

                        return new ApiResponse { Success = false, Message = "BURAYA YETKİNİZ BULUNMAMAKTA" };
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



