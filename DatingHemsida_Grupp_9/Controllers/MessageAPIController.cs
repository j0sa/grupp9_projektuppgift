using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatingHemsida_Grupp_9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageAPIController : ControllerBase
    {
        private readonly DatingContext _DatingContext;

        public MessageAPIController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }

        /*
         * Skickar meddelanden
         * Tar emot meddelandet och mottagaren från vyn och skapar ett nytt meddelande som skickas till databasen
         */

        [HttpPost]
        [Route("postmessage")]
        public async Task SendMessage([FromBody] Message message)
        {
            int user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name).Id;
            DataLayer.Models.Message databaseMessage = new DataLayer.Models.Message()
            {
                SenderId = user,
                ReceiverId = message.ReceiverId,
                Text = message.Text,
                Date = DateTime.Now
            };
            _DatingContext.Messages.Add(databaseMessage);
            await _DatingContext.SaveChangesAsync();
        }
    }
}