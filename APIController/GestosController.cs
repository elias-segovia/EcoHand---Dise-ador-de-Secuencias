using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using APIController.Responses;
using APIController.Model;
using Newtonsoft.Json;

namespace APIController
{
    public class GestosController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Gestos";

        public static async Task<GetGestoByIdResponse> GetGestosById(int id)
        {


            using (var response = await _httpClient.GetAsync(controller + "/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    GetGestoByIdResponse result = new GetGestoByIdResponse();
                    string res = await response.Content.ReadAsStringAsync();
                    GestoModel gesto = new GestoModel();
                    JsonConvert.PopulateObject(res, gesto);
                    result.Result = gesto;
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public static async Task<GetGestoPorNombreResponse> GetGestoPorNombre(string nombre)
        {
            GetGestoPorNombreResponse result = new GetGestoPorNombreResponse();
            using (var response = await _httpClient.GetAsync(controller + "?nombreGesto=" + nombre))
            {
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    if (!res.Equals("null"))
                    {
                        GestoModel gesto = new GestoModel();

                        JsonConvert.PopulateObject(res, gesto);

                        result.Gesto = gesto;

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

        public static async Task EditarAsync(GestoModel gesto)
        {
            using (var response = await _httpClient.PutAsJsonAsync(controller, gesto))
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

        public static async Task Delete(int id)
        {
            using(var response = await _httpClient.DeleteAsync(controller + '/' + id))
            {
                if (!response.IsSuccessStatusCode)
                {
                    //manejar el error
                    throw new Exception(response.ReasonPhrase);

                }
             
            }
        }

        public static async Task<GetGestosResponse> GetGestos()
        {


            using (var response = await _httpClient.GetAsync(controller))
            {
                if (response.IsSuccessStatusCode)
                {
                    GetGestosResponse result = new GetGestosResponse();
                    string res = await response.Content.ReadAsStringAsync();
                    List<GestoModel> gestos = new List<GestoModel>();
                    JsonConvert.PopulateObject(res, gestos);
                    result.Result = gestos;
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<GetGestosByUserIdResponse> GetGestosByUserId(int userId)
        {
            using (var response = await _httpClient.GetAsync(controller + "?UserId=" + userId))
            {
                if (response.IsSuccessStatusCode)
                {
                    GetGestosByUserIdResponse result = new GetGestosByUserIdResponse();
                    string res = await response.Content.ReadAsStringAsync();
                    List<GestoModel> gestos = new List<GestoModel>();
                    JsonConvert.PopulateObject(res, gestos);
                    result.Result = gestos;
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public static async Task PostAsync(GestoModel gesto)
        {

            using (var response = await _httpClient.PostAsJsonAsync<GestoModel>(controller, gesto))
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
