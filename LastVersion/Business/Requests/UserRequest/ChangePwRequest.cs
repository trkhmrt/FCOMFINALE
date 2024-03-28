using System;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;
using System.Net.Http;

namespace Business.Requests
{
	public class ChangePwRequest
	{
        public async Task<ApiResponse> ChangePw(UserPwUpdateDto model,string token)
        {

            using (var client = new HttpClient())
            {
                

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                var response = await client.PostAsync("https://localhost:7069/user/changepw", content);

               



                if (response.IsSuccessStatusCode)
                {
                    
                    var succeded = await response.Content.ReadAsStringAsync();
                   



                    return new ApiResponse { Success = true, Message = succeded };


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
                    return new ApiResponse { Success = false, Message = response.ReasonPhrase };
                }

            }
        }








    }
}

