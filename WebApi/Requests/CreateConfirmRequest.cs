using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class CreateConfirmRequest
    {
        [Required]
        [JsonProperty(PropertyName = "number")]
        public int Number { get; set; }
        [Required]
        [JsonProperty(PropertyName = "email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Mail { get; set; }
    }
}
