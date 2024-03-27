using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Azure;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Business.Requests.TokenRequest
{
    public class RefreshTokenRequest
    {

        public async Task<(ApiResponse,string)> CheckToken(string refreshToken)
        {


           

            
            var jsonToken = JsonConvert.SerializeObject(refreshToken);

          
            var content = new StringContent(jsonToken, Encoding.UTF8, "application/json");

           
            using (var client = new HttpClient())
            {
                var apiUrl = "https://localhost:7069/token/checktoken";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);

                var response = await client.PostAsync(apiUrl, content);

                
                 if (response.IsSuccessStatusCode)
                {
                    var contentToken = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<string>(contentToken);

                    




                    return (new ApiResponse { Success = true, Message = "TOKEN CHANGED" },tokenResponse);
                }

                else
                {
                   
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (new ApiResponse { Success = false, Message = "TOKEN NOT CHANGED.REFRESH TOKEN EXP"},"");
                }
            }
        }



    }

}

