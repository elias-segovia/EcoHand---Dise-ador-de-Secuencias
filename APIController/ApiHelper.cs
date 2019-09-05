using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace APIController
{
    public class ApiHelper
    {
        private static HttpClient apiClient;

        public static HttpClient GetApiClient()
        {
            return apiClient;
        }

        private static void SetApiClient(HttpClient value)
        {
            apiClient = value;
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Cambiar por la url de Azure
            //https://ecohand-backend.azurewebsites.net/
            //http://localhost:56303/
            apiClient.BaseAddress = new Uri("https://ecohand-backend.azurewebsites.net/");

        }


        public static HttpClient EnsureHttpClientCreated()
        {
            if (apiClient == null)
            {
                
                SetApiClient(new HttpClient());
                
            }
            return GetApiClient();
                
        }


    }
}
