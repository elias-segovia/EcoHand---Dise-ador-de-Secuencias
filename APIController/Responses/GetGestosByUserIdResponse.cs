using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIController.Responses
{
    public class GetGestosByUserIdResponse
    {
        public List<GestoModel> Result { get; set; }
    }
}
