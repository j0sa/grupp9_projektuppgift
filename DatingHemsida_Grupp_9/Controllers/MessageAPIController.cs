using DataLayer;
using DatingHemsida_Grupp_9.Models;
using DatingHemsida_Grupp_9.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/<MessageController>
        //[HttpGet]
        //public IEnumerable<string> GetAllMessages()
        //{
        //var UserName = User.Identity.Name;
        //var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

        //var messages = _DatingContext.Messages.ToList();
        //var mot = messages.Where(x => x.ReciverId == 16).ToList();

        //var profiles = _DatingContext.Profiles.ToList();

        //Profile profile = new Profile()
        //{
        //    Id = user.Id,
        //    Firstname = user.Firstname,
        //    Lastname = user.Lastname,
        //    Age = user.Age,
        //    Email = user.Email,
        //    Gender = user.Gender,
        //    SexualOrientation = user.SexualOrientation,
        //    ImagePath = user.ImagePath
        //};

        //List<Message> messages1 = new List<Message>()
        //{
        //};
        //foreach (var m in mot)
        //{
        //    Message message = new Message()
        //    {
        //        MessageId = m.MessageId,
        //        SenderId = m.SenderId,
        //        ReciverId = m.ReciverId,
        //        Text = m.Text,
        //        Date = m.Date,
        //        Author = profiles.Single(x => x.Id == m.SenderId).Firstname

        //    };
        //    messages1.Add(message);

        //};

        //WallViewModel wallViewModel = new WallViewModel()
        //{
        //    Profile = profile,
        //    WallMessages = messages1

        //};

        //return wallViewModel;
        //}

        // GET api/<MessageController>/5
        //[HttpGet]
        //[Route("printmessage")]
        //public string PrintMessage()
        //{
        //    string message = "test";
        //    return message;
        //}

        // POST api/<MessageController>
        [HttpPost]
        [Route("postmessage")]
        public void SendMessage([FromBody] Message message)
        {

            var UserName = User.Identity.Name;
            int user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName).Id;

            DataLayer.Models.Message databaseMessage = new DataLayer.Models.Message()
            {
                SenderId = user,
                ReciverId = message.ReciverId,
                Text = message.Text,
                Date = DateTime.Now
            };
            _DatingContext.Messages.Add(databaseMessage);
            _DatingContext.SaveChanges();

            

        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}