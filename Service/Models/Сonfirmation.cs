using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{

    [Table("confirmation")]
  public  class Confirmation: Base
    {

        [ForeignKey("Email")]
        [Column(name: "email")]
        public string Email { get; set; }

        [Column(name: "random_number")]
        public int Rand { get; set; }

      
    }
}
