using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Models
{
    public class Message
    {
      
        public int MessageId { get; set; }

        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey("SenderId")]
        public Profile Sender { get; set; }

        [ForeignKey("ReciverId")]
        public Profile Reciver { get; set; }

    }
}
