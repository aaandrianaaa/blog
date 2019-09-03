using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{
   public class Base
    {
        [JsonProperty(PropertyName = "id")]
        [Column("id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "deleted_at")]
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
