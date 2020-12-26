using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Friend
    {
        [Key]
      public virtual int Id { get; set; }

        [NotMapped]
        [ForeignKey ("Sender")]
      public virtual Profile Sender { get; set; }
        
        //[NotMapped]
        //[ForeignKey("Reciver")]
        //public virtual Profile Reciver { get; set; }

        public DateTime? Date { get; set; }
        public bool Status { get; set; }
    }
    
}
