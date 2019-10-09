using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIController.Model;
using Newtonsoft.Json;

namespace APIController
{
    public class SecuenciaController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Secuencias";

        
        public static async Task<List<Secuencia>> Get()
        {
            using (var response = await _httpClient.GetAsync(controller))
            {
                if (response.IsSuccessStatusCode)
                {
                    
                    string res = await response.Content.ReadAsStringAsync();
                    List<Secuencia> secuencias = new List<Secuencia>();
                    JsonConvert.PopulateObject(res, secuencias);
                    
                    return secuencias;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task Delete(int id)
        {

        }

        public static async Task Put(Secuencia secu)
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
