using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatingHemsida_Grupp_9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MessageController>
        [HttpPost]
        [Route("postmessage")]
        public void SendMessage(int senderId, int recierId, string text, DateTime date)
        {
            Message message = new Message() {
                SenderId = senderId,
                ReciverId = recierId,
                Text = text,
                Date = DateTime.Now
            };

            _DatingContext.Add(message);
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