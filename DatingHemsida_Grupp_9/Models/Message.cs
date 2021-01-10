using System;

namespace DatingHemsida_Grupp_9.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}