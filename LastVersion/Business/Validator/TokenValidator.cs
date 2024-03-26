using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace BusinessLayer.Validator
{
	public class TokenValidator
	{

        

        public async Task<bool> Validate(string token)
		{
          
            using var client = new HttpClient();

         

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7069/token/checktoken");
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

           
            HttpResponseMessage response = await client.SendAsync(request);

            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {

                string responseData = await response.Content.ReadAsStringAsync();

                return false;
            }
            else
            {
               
                return true;
            }

        }


    }
}

