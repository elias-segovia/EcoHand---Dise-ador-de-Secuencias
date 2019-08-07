using APIController.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace APIController.Responses
{
    public class GetGestoByIdResponse
    {
        public GestoModel Result { get; set; }
               
    }
}
