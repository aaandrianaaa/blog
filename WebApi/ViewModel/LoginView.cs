﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoginView
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
