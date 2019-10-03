using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIController.Model;

namespace APIController
{
    public class SecuenciaController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Secuencia";

        
        public static async Task Get()
        {

        }

        public static async Task Delete(int id)
        {

        }

        public static async Task Put()
        {

        }

        public static async Task GetById()
        {

        }

        public static async Task PostAsync(Secuencia secu)
        {
            using (var response = await _httpClient.PostAsJsonAsync<Secuencia>(controller, secu))
            {
                if (response.IsSuccessStatusCode)
                {
                    //todo ok
                }
                else
                {
                   throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
