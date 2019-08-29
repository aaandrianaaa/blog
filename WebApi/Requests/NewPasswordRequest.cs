using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class NewPasswordRequest
    {
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "old_password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "new_password")]
        public string NewPassword { get; set; }
     
    }
}
