using System;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using DtoLayer.Concrete;

namespace Business.Requests
{
	public class UserDeleteRequest
	{

        public async Task<ApiResponse> DeleteUser(string id, string token)
        {



            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(id);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("https://localhost:7069/user/delete", content);





                if (response.IsSuccessStatusCode)
                {


                    return new ApiResponse { Success = true, Message = "Silme Başarılı" };


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


