using System;
using System.Net.Http.Headers;
using System.Text;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Business.Requests.TokenRequest
{
    public class RefreshTokenRequest
    {

        public async Task<ApiResponse> CheckToken(string refreshToken)
        {


           

            
            var jsonToken = JsonConvert.SerializeObject(refreshToken);

          
            var content = new StringContent(jsonToken, Encoding.UTF8, "application/json");

           
            using (var client = new HttpClient())
            {
                var apiUrl = "https://localhost:7069/checktoken";


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);

                var response = await client.PostAsync(apiUrl, content);

                
                 if (response.IsSuccessStatusCode)
                {
                    
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse { Success = true, Message = "TOKEN CHANGED" };
                }

                else
                {
                   
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ApiResponse { Success = true, Message = "TOKEN NOT CHANGED.REFRESH TOKEN EXP" };
                }
            }
        }



    }

}

