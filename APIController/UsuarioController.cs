using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace APIController
{
    class UsuarioController
    {
        private static HttpClient _httpClient = ApiHelper.EnsureHttpClientCreated();

        private static string controller = "api/Usuarios";
    }
}
