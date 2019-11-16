using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIController.Model;
using Newtonsoft.Json;
using APIController.Responses;

namespace APIController
{
    public class SecuenciaController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Secuencias";


        public static async Task<List<Secuencia>> Get()
        {
            using (var response = await _httpClient.GetAsync(controller + "/get/"))
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

        public static async Task<GetSecuenciaPorNombreResponse> GetSecuenciaPorNombreAsync(string nombre)
        {
            GetSecuenciaPorNombreResponse result = new GetSecuenciaPorNombreResponse();
            using (var response = await _httpClient.GetAsync(controller + "?secuenciaName=" + nombre))
            {
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    if (!res.Equals("null"))
                    {
                        Secuencia sec = new Secuencia();

                        JsonConvert.PopulateObject(res, sec);

                        result.Secuencia = sec;

                        result.Exitoso = true;
                    }
                }
                else
                {

                    result.Exitoso = false;

                }

                return result;
            }
        }

        public static async Task Delete(int id)
        {
            using (var response = await _httpClient.DeleteAsync(controller + '/' + id))
            {
                if (!response.IsSuccessStatusCode)
                {
                    //manejar el error
                    throw new Exception(response.ReasonPhrase);

                }

            }
        }

        public static async Task Put(Secuencia secu)
        {
            using (var response = await _httpClient.PutAsJsonAsync(controller, secu))
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
