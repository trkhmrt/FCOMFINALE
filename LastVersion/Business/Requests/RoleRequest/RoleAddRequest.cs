using System;
using Newtonsoft.Json.Linq;

namespace Business.Requests
{
	public class RoleAddRequest
	{
        public async Task<bool> AddRole(string roleName)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // API endpointini ve string parametreyi URL'de belirtin
                    string apiUrl = $"https://localhost:7069/role/addrole/{roleName}";

                    // HTTP GET isteği oluştur
                    var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    // Token'ı Authorization başlığına ekle
                    //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    // İstek gönder
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Rol ekleme isteği başarısız oldu. Durum kodu: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata oluştu: " + ex.Message);
                }
                return false;
            }
        }
    }
}

