using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class ArticleReitingRequest
    {
       
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
    }
}
