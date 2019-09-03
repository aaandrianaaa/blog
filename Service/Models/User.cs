using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Service.Models
{
    [Table("users")]
    public class User: Base
    {
        public static object Claims { get; internal set; }
    
        [Column(name: "first_name")]
        public string FirstName { get; set; }
        [Column(name: "last_name")]
        public string LastName { get; set; }
        [Column(name: "nickname")]
        public string Nickname { get; set; }
        [Column(name: "email")]
        public string Email { get; set; }
        [Column(name: "about_user")]
        public string AboutUser { get; set; }
        [Column(name: "password")]
        [JsonIgnore]
        public string Password { get; set; }
        [Column(name: "age")]
        public int? Age { get; set; }
        [Column(name: "birthday_date")]
        public DateTime? BirthdayDate { get; set; }
        [Column(name: "role_id")]
       
        public int RoleID { get; set; } = 1;
        [Column(name: "role")]
       
        public Role Role { get; set; }
        
        [Column(name: "activated")]
        public bool Activated { get; set; } = false;
      

        [Column(name: "blocked")]
        public bool Blocked { get; set; } = false;

        [Column (name: "blocked_until")]

        public DateTime? BlockedUntil { get; set; }
       
    }
}
