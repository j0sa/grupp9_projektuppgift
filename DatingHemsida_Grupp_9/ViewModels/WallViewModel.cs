using DatingHemsida_Grupp_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.ViewModels
{
    public class WallViewModel
    {
        public Profile Profile { get; set; }

        public List<Message> WallMessages { get; set; } = new List<Message>();

    }
}
