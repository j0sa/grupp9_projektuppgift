using DatingHemsida_Grupp_9.Models;
using System.Collections.Generic;

namespace DatingHemsida_Grupp_9.ViewModels
{
    public class WallViewModel
    {
        public Profile Profile { get; set; }

        public List<Message> WallMessages { get; set; } = new List<Message>();

       
    }
}