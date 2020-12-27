using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("SenderId")]
        public virtual Profile Sender { get; set; }

        [ForeignKey("ReciverId")]
        public virtual Profile Reciver { get; set; }
    }
}
