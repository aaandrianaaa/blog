using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Service.Models
{
    [Table("likes")]
    public class Like
    {
        [Column(name: "id")]
        public int Id { get; set; }
        [Column(name: "user_id")]
        public int UserId { get; set; }
        [Column(name: "user")]
        public User User { get; set; }
        [Column(name: "like")]
        public bool LikeIt { get; set; }
        [Column(name: "comment")]
        public Comment Comment { get; set; }
        [Column(name: "comment_id")]
        public int CommentId { get; set; }
    }
}
