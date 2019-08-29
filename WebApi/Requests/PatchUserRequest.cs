using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class PatchUserRequest
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }
        [JsonProperty(PropertyName = "about_user")]
        public string AboutUser { get; set; }
        [JsonProperty(PropertyName = "age")]
        public int? Age { get; set; }
        [JsonProperty(PropertyName = "birthday_date")]
        public DateTime BirthdayDate { get; set; }
       
    }
}
