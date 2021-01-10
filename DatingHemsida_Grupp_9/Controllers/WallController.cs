using DataLayer;
using DatingHemsida_Grupp_9.Models;
using DatingHemsida_Grupp_9.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public ActionResult Index(int? profileId)
        // GET: Wall
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

        //Kontrollerar navbar
        public void FriendRequestVisible()
        {
            //Skickar true or false till vyn för att visa om det finns vänförfrågningar
            ViewBag.Requests = false;
            var user = User.Identity.Name;

            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));

            var id = profile.Id;

            //Lista av vänförfrågningar
            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReceiverId.Equals(id))
                .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

            //Om listan av vänförfrågningar är större än 0
            if (listatva.Count > 0)
            {
                ViewBag.Requests = true;
            }
        }

        [HttpGet]
        public PartialViewResult DisplayMessage(Message message)
        {
            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName).Id;

            var databaseMessages = _DatingContext.Messages.Where(x => x.ReceiverId == message.ReceiverId);
            //Hämtar alla profiler i databas
            var profiles = _DatingContext.Profiles.ToList();

            List<Message> messages = new List<Message>();

            foreach (var m in databaseMessages)
            {
                Message message1 = new Message()
                {
                    MessageId = m.MessageId,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Text = m.Text,
                    Date = m.Date,
                    Author = profiles.Single(x => x.Id == m.SenderId).Firstname + " " + profiles.Single(x => x.Id == m.SenderId).Lastname
                };
                messages.Add(message1);
            };
            return PartialView("_Messeges", messages);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Wall/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Wall/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Wall/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Wall/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Wall/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}