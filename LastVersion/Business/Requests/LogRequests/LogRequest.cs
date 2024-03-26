using System;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Business.Requests.LogRequest
{
    public class LogRequest
    {
        public async Task<(List<UserLog>, ApiResponse)> GetAllLogs(string token)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {


                    // API endpointini buraya yazın
                    string apiUrl = "https://localhost:7069/log/getlogs";
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
                            var loglist = JsonConvert.DeserializeObject<List<UserLog>>(responseData);

                            return (loglist, new ApiResponse { Success = true, Message = "BAŞARILI" });
                        }
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {


                                return (null, new ApiResponse { Success = false, Message = "TOKEN GEÇERLİ DEĞİL TEKRARDAN GİRİŞ YAPIN YADA TOKEN YENİLEYİN" });
                            }
                            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                            {

                                return (null, new ApiResponse { Success = false, Message = "BURAYA YETKİNİZ BULUNMAMAKTA" });
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

