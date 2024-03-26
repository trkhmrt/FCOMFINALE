using System;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Requests
{
	public class UserDeleteRequest
	{

		public async Task<bool> DeleteUserById(Guid id)
		{

            using (var httpClient = new HttpClient())
            {
                try
                {
                   
                    string apiUrl = $"https://localhost:7069/user/delete/{id}";

                   
                    using (var response = await httpClient.DeleteAsync(apiUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata oluştu: " + ex.Message);
                }
            }
            return false;
        }

		}


	}


