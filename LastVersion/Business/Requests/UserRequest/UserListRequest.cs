using System;
using System.Net.Http.Headers;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Business.Requests
{
    public class UserListRequest
    {

        public async Task<(List<User>,ApiResponse)> GetAllUsers(string token)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                  

                    // API endpointini buraya yazın
                    string apiUrl = "https://localhost:7069/user/listuser";
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    using (var response = await httpClient.GetAsync(apiUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Başarılı istek durumunda veriyi al
                            var responseData = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("API Response:");
                            var userList = JsonConvert.DeserializeObject<List<User>>(responseData);
                           
                            return (userList ,new ApiResponse { Success = true, Message = "BAŞARILI" }); 
                        }
                        else
                        {
                             if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {

                                
                                return (null,new ApiResponse { Success = false, Message = "TOKEN GEÇERLİ DEĞİL TEKRARDAN GİRİŞ YAPIN YADA TOKEN YENİLEYİN" });
                            }
                            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                            {

                                return (null,new ApiResponse { Success = false, Message = "BURAYA YETKİNİZ BULUNMAMAKTA" });
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata oluştu: " + ex.Message);
                   
                }
                return (null, new ApiResponse { Success = false, Message = "HATA MEYDANA GELDİ" });
            }




        }
    }
}

