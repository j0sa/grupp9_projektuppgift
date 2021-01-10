using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingHemsida_Grupp_9.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [RegularExpression(@"^[^±!@£$%^&*_+§¡€#¢§¶•ªº«\/<>?:;|=.,0-9]{1,20}$", ErrorMessage = "Invalid Firstname")]
        public String Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [RegularExpression(@"^[^±!@£$%^&*_+§¡€#¢§¶•ªº«\/<>?:;|=.,0-9]{1,20}$", ErrorMessage = "Invalid Lastname")]
        public String Lastname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 100, ErrorMessage = "Minimum age is 18")]
        public int Age { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Sexual Orientation is a required field")]
        [Display(Name = "Sexual Orientation")]
        public string SexualOrientation { get; set; }

        public bool Active { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        public Profile()
        {
        }
    }
}