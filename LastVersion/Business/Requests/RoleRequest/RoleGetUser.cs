using System;
using EntityLayer.Concrete;
using System.Text.Json;
using Newtonsoft.Json;

namespace Business.Requests.RoleRequest
{
	public class RoleGetUser
	{
        public async Task<string> GetUserRole(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // API endpoint URL'sini oluşturun
                    string apiUrl = $"https://localhost:7069/role/getuserrole/{userId}"; 

                    // HTTP GET isteği oluşturun ve yanıtı alın
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Yanıtı kontrol edin
                    if (response.IsSuccessStatusCode)
                    {
                        // Yanıtı okuyun ve ekrana yazdırın
                        var content = await response.Content.ReadAsStringAsync();

                        Console.WriteLine(content);

                       

                        return content;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Kullanıcı bulunamadı.");
                        
                    }
                    else
                    {
                        Console.WriteLine("API isteği başarısız oldu. Durum kodu: " + response.StatusCode);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("HTTP isteği başarısız oldu: " + e.Message);
                }
                return null;
            }
        }
    }
}

