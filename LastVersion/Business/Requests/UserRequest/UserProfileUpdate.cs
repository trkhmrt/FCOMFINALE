using System;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Requests.UserRequest
{
	public class UserProfileUpdate
	{
        public async Task<ApiResponse> UserUpdate(NormalUserUpdateDto model, string token)
        {

            using (var client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsync("https://localhost:7069/user/normaluser", content);


                if (response.IsSuccessStatusCode)
                {

                    var succeded = await response.Content.ReadAsStringAsync();

                    return new ApiResponse { Success = true, Message = succeded };


                }
                else
                {

                    var error = await response.Content.ReadAsStringAsync();

                    return new ApiResponse { Success = false, Message = response.ReasonPhrase };
                }
            }
        }
    }
}

