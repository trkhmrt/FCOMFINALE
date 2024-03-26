using System;
using EntityLayer.Concrete;
using System.Text.Json;
using Newtonsoft.Json;

namespace Business.Requests.RoleRequest
{
    public class RoleGetAll
    {
        public async Task<List<Role>> GetAllRoles()
        {
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    // API endpoint URL'sini oluştur
                    string apiUrl = "https://localhost:7069/role/getallroles";

                    // HttpClient nesnesi oluştur
                    using (var httpClient = new HttpClient())
                    {
                        // HTTP GET isteği oluştur
                        var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                        // Gerekirse, Authorization header ekleyin
                        // ...

                        // API'yi çağır ve yanıtı al
                        var response = await httpClient.SendAsync(request);

                        // Yanıt içeriğini oku
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();

                            // JSON yanıtını işle
                            var roles = JsonConvert.DeserializeObject<List<Role>>(content);

                            // Rolleri listele
                            foreach (var role in roles)
                            {
                                Console.WriteLine(role);
                            }
                            return roles;
                        }
                        else
                        {
                            Console.WriteLine("API hatası: " + response.StatusCode);
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                    return null;
                }


            }

            }
        }
    }


