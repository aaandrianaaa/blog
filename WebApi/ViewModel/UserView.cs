using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class UserView
    {
        
        public int ID { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Nickname { get; set; }
    
        public string AboutUser { get; set; }
     
        public int? Age { get; set; }
   
        public DateTime BirthdayDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Avatar { get; set; }
    }
}
