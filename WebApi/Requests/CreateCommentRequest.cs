using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class CreateCommentRequest
    {
        [JsonProperty (PropertyName ="text")]
        public string Text { get; set; }
    
    }
}
