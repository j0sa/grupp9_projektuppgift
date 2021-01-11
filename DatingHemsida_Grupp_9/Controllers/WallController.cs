using DataLayer;
using DatingHemsida_Grupp_9.Models;
using DatingHemsida_Grupp_9.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DatingHemsida_Grupp_9.Controllers
{
    [Authorize]
    public class WallController : Controller
    {
        private readonly DatingContext _DatingContext;

        public WallController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }

        /*
         * Sidan där man kan se sin egen och andras väggar med meddelanden.
         * Om parameter ej skickas med visas den egna väggen, samt om man söker på sitt namn visar man egna profilen fast via parameter.
         * Skickas en parameter med visas den användarens vägg.
         */

        public ActionResult Index(int? profileId)
        {
            var shownProfile = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            ViewBag.AddFriend = false;
            if (User.Identity.Name != null && shownProfile != null)
            {
                FriendRequestVisible();
                var messageList = new List<DataLayer.Models.Message>();
                if (profileId != null)
                {
                    AddFriendVisible((int)profileId);
                    messageList = _DatingContext.Messages.Where(x => x.ReceiverId == profileId).ToList();
                    shownProfile = _DatingContext.Profiles.SingleOrDefault(p => p.Id == profileId);
                }
                else
                {
                    messageList = _DatingContext.Messages.Where(x => x.ReceiverId == shownProfile.Id).ToList();
                }
                //Skapar ny profil för den som ska visas
                Profile profile = new Profile()
                {
                    Id = shownProfile.Id,
                    Firstname = shownProfile.Firstname,
                    Lastname = shownProfile.Lastname,
                    Age = shownProfile.Age,
                    Email = shownProfile.Email,
                    Gender = shownProfile.Gender,
                    SexualOrientation = shownProfile.SexualOrientation,
                    ImagePath = shownProfile.ImagePath
                };
                var profiles = _DatingContext.Profiles.ToList();
                List<Message> currentMessages = new List<Message>();
                //Fyller tomma listan med nya meddelanden från databasen och ger dem en author genom
                //att jämföra SenderId med profilerna i profiles-listan
                foreach (var m in messageList)
                {
                    Message message = new Message()
                    {
                        MessageId = m.MessageId,
                        SenderId = m.SenderId,
                        ReceiverId = m.ReceiverId,
                        Text = m.Text,
                        Date = m.Date,
                        Author = profiles.Single(x => x.Id == m.SenderId).Firstname + " " + profiles.Single(x => x.Id == m.SenderId).Lastname
                    };
                    currentMessages.Add(message);
                };
                //Skapar en viewmodel och fyller den med profil och profilens meddelanden
                WallViewModel wallViewModel = new WallViewModel()
                {
                    Profile = profile,
                    WallMessages = currentMessages
                };
                return View(wallViewModel);
            }
            //Om ej inloggad och ej profil
            else { return RedirectToAction("Index", "Home"); }
        }

        /*
         * Bestämmer om knappen add friend ska vara synlig
         */

        public void AddFriendVisible(int id)
        {
            var UserName = User.Identity.Name;
            int user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName).Id;

            var listOfFriends = _DatingContext.FriendRequests.Where(x => x.FriendSenderId == id && x.FriendReceiverId == user ||
             x.FriendReceiverId == id && x.FriendSenderId == user).ToList();

            if (user != id)
            {
                if (!listOfFriends.Any())
                {
                    ViewBag.AddFriend = true;
                };
            }
        }

        /*
         * Bestämmer vilken text som ska visas i navbar baserat på om den inloggade användaren fått nya vänförfrågningar.
         */

        public void FriendRequestVisible()
        {
            ViewBag.Requests = false;
            var user = User.Identity.Name;
            var id = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user)).Id;
            var listNotAccepted = _DatingContext.FriendRequests.Where(x => x.FriendReceiverId.Equals(id))
                .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();
            if (listNotAccepted.Count > 0)
            {
                ViewBag.Requests = true;
            }
        }

        /*
         * Uppdaterar partialview för meddelanden
         */

        [HttpGet]
        public PartialViewResult DisplayMessage(Message message)
        {
            var databaseMessages = _DatingContext.Messages.Where(x => x.ReceiverId == message.ReceiverId);
            var profiles = _DatingContext.Profiles.ToList();
            List<Message> messages = new List<Message>();
            foreach (var i in databaseMessages)
            {
                Message newMessage = new Message()
                {
                    MessageId = i.MessageId,
                    SenderId = i.SenderId,
                    ReceiverId = i.ReceiverId,
                    Text = i.Text,
                    Date = i.Date,
                    Author = profiles.Single(x => x.Id == i.SenderId).Firstname + " " + profiles.Single(x => x.Id == i.SenderId).Lastname
                };
                messages.Add(newMessage);
            };
            return PartialView("_Messeges", messages);
        }
    }
}