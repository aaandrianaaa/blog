using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }
        [Required]
        [JsonProperty(PropertyName = "email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty(PropertyName = "confirm_password")]
        public string ConfirmPassword { get; set; }

        [JsonProperty(PropertyName = "about_user")]
        public string AboutUser { get; set; }

        [JsonProperty(PropertyName = "birthday_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? BirthdayDate { get; set; }
    }
}
