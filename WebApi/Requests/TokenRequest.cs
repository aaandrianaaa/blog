using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class TokenRequest
    {
        [Required]
        [JsonProperty(PropertyName = "email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [JsonProperty(PropertyName = "password")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
