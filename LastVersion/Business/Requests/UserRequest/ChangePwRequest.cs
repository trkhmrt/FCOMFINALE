using System;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;

namespace Business.Requests
{
	public class ChangePwRequest
	{
        public async Task<ApiResponse> ChangePw(UserPwUpdateDto model)
        {

            using (var client = new HttpClient())
            {
                

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

              

                var response = await client.PostAsync("https://localhost:7069/user/changepw", content);

               



                if (response.IsSuccessStatusCode)
                {
                    //Buraya Cevap başarılı dönebilir.Başarısızda olduğu gibi
                    var succeded = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(succeded);



                    return new ApiResponse { Success = true, Message = succeded };


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

