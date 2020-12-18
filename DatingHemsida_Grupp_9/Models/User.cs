using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Required field")]
        public String Namn { get; set; }

       [Display(Name ="Detta är ett email fällt")]
        public String Email { get; set; }

    }
}
