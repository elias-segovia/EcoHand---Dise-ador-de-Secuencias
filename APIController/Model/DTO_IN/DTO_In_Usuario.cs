using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIController.Model.DTO_IN
{
    public class DTO_In_Usuario
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("contraseña")]
        public string Contraseña { get; set; }

        public DTO_In_Usuario(string username, string contraseña)
        {
            Username = username;
            Contraseña = contraseña;
        }

        public DTO_In_Usuario(string username, string contraseña, string mail)
        {
            Username = username;
            Contraseña = contraseña;
            Email = mail;
        }
    }
}
