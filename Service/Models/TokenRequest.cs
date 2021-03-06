﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    public class TokenRequest
    {
       [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
