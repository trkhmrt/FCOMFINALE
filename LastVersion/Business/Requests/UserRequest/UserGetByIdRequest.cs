using System;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Business.Requests
{
	public class UserGetByIdRequest
	{
        public async Task<UserUpdateDto> GetUserById(string userId,string token)
        {
             using (var httpClient = new HttpClient())
                {
                    try
                    {


                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                   
                    string apiUrl = $"https://localhost:7069/user/{userId}";

                        using (var response = await httpClient.GetAsync(apiUrl))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // Başarılı istek durumunda veriyi al
                                var responseData = await response.Content.ReadAsStringAsync();

                        
                            var user = JsonConvert.DeserializeObject<UserUpdateDto>(responseData);






                            return user;
                            }
                        else
                            {
                                Console.WriteLine("API isteği başarısız oldu. Durum kodu: " + response.StatusCode);
                            Console.WriteLine(response.StatusCode.GetType().Name);
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata oluştu: " + ex.Message);
                        
                    }
                }

            return null;
        }
        }
    }


