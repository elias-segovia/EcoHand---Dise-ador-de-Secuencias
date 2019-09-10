using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using APIController.Model.DTO_IN;
using APIController.Model;
using Newtonsoft.Json;
using System.Net;

namespace APIController
{
    public class UsuarioController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Usuarios";

        public static async Task<bool> PostAsync(DTO_In_Usuario user)
        {

            using (var response = await _httpClient.PostAsJsonAsync<DTO_In_Usuario>(controller + "/Login", user))
            {

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                   
                    var errorCode = JsonConvert.DeserializeObject<ErrorModel>(result);

                    if(errorCode.Message == ErrorCodes.USUARIO_INEXISTENTE)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return false;
        }

        public static async Task<bool> RegitroAsync(DTO_In_Usuario user)
        {

            using (var response = await _httpClient.PostAsJsonAsync<DTO_In_Usuario>(controller + "/User", user))
            {

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {

                    var errorCode = JsonConvert.DeserializeObject<ErrorModel>(result);

                    if (errorCode.Message == ErrorCodes.USUARIO_EXISTENTE)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return false;
        }

    }
}
