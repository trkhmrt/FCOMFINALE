using System;
using Newtonsoft.Json;
using System.Text;
using DtoLayer.Concrete;

namespace Business.Requests.RoleRequest
{
    public class RoleUpdateRequest
    {
        public async Task AddRoleWithList(RoleUpdateRequestDto roleUpdateRequestDto)
        {
            // API endpoint URL'si
            string apiUrl = "https://localhost:7069/role/updaterole";

            // JSON modelin string karşılığını oluştur
            string jsonContent = JsonConvert.SerializeObject(roleUpdateRequestDto);

            // HttpClient nesnesi oluştur
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // HTTP POST isteği oluştur
                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

                    // Content-Type header'ı ekle
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Token ekleme (gerekirse)
                    //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    // İsteği gönder
                    var response = await httpClient.SendAsync(request);

                    // Yanıt durum kodu kontrol et
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Rol ekleme başarılı!");
                    }
                    else
                    {
                        Console.WriteLine("Rol ekleme başarısız: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
   
}

