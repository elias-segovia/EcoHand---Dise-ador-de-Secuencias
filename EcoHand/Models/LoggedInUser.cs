﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public class LoggedInUser : ILoggedInUser
    {

        public string UserName { get; set; }

        public int Id { get; set; }
    }
}
