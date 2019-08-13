using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{
    [Table("roles")]
  public  class Role
    {
        [ForeignKey("RoleID")]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
