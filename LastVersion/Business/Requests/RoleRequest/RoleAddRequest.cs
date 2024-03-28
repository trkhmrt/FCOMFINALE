using System;
using Azure;
using EntityLayer.Concrete;
using Newtonsoft.Json.Linq;

namespace Business.Requests
{
	public class RoleAddRequest
	{
        public async Task<ApiResponse> AddRole(string roleName,string token)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                   
                    string apiUrl = $"https://localhost:7069/role/addrole/{roleName}";

                  
                    var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                   
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                  
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return new ApiResponse { Success = false, Message = "Rol Ekleme başarılı" };
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
                catch (Exception ex)
                {
                    Console.WriteLine("Hata oluştu: " + ex.Message);

                    return new ApiResponse { Success = false, Message = ex.Message  };
                }
                
            }
        }
    }
}

