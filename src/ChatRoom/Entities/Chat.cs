using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Entities
{
    public class Chat
    {
        [Key]
        [Required]
        public int IdChat { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
