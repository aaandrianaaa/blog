using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class NewRoleRequest
    {
        [JsonProperty(PropertyName = "new_role")]
        public int Role { get; set; }
    }
}
