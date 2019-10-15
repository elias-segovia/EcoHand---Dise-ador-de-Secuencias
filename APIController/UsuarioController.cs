﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using APIController.Model.DTO_IN;
using APIController.Model;
using Newtonsoft.Json;
using System.Net;
using APIController.Model.DTO_Out;

namespace APIController
{
    public class UsuarioController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Usuarios";

        public static async Task<Dto_Out> PostAsync(DTO_In_Usuario user)
        {
            Dto_Out resp = new Dto_Out();
            using (var response = await _httpClient.PostAsJsonAsync<DTO_In_Usuario>(controller + "/Login", user))
            {

                var result = await response.Content.ReadAsStringAsync();

                

                JsonConvert.PopulateObject(result, resp);

                resp.Successfull = true;

                if (response.IsSuccessStatusCode)
                {
                    return resp;
                }
                else if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                   
                    var errorCode = JsonConvert.DeserializeObject<ErrorModel>(result);

                    if(errorCode.Message == ErrorCodes.USUARIO_INEXISTENTE)
                    {
                        resp.Successfull = false;
                        return resp;
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return resp;
        }

        public static async Task<Dto_Out> RegitroAsync(DTO_In_Usuario user)
        {
            Dto_Out resp = new Dto_Out();
            using (var response = await _httpClient.PostAsJsonAsync<DTO_In_Usuario>(controller + "/User", user))
            {

                var result = await response.Content.ReadAsStringAsync();

                JsonConvert.PopulateObject(result, resp);

                resp.Successfull = true;

                if (response.IsSuccessStatusCode)
                {
                    return resp;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    resp.Successfull = false;
                    var errorCode = JsonConvert.DeserializeObject<ErrorModel>(result);

                    if (errorCode.Message == ErrorCodes.USUARIO_EXISTENTE)
                    {
                        return resp;
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return resp;
        }

    }
}
