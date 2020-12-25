using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey ("Author")]
        public int AuthorId { get; set; }
        public Profile Author { get; set; }

        [ForeignKey("Recipient")]
        public int ToId { get; set; }
        public Profile Recipient { get; set; }

    }
}
