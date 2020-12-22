using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Namn { get; set; }

        
        public String Email { get; set; }
    }
}
