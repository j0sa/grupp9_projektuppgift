using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("SenderId")]
        public Profile Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public Profile Receiver { get; set; }
    }
}