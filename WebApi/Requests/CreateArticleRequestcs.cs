using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests
{
    public class CreateArticleRequestcs
    {
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [Required]
        [JsonProperty(PropertyName = "category_id")]
        public int CategoryID { get; set; }


    }
}
