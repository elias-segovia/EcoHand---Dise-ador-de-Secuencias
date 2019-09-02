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
            apiClient.BaseAddress = new Uri("http://localhost:56303/");

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
