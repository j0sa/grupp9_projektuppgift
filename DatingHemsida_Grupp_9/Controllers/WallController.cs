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
            //Hämtar användaren
            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

            ViewBag.AddFriend = false;

            //Om inloggad och egen profil skapad
            if (UserName != null && user != null)
            {
                FriendRequestVisible();

                //Hämtar alla meddelanden
                var messages = _DatingContext.Messages.ToList();

                //Skapar en tom lista av meddelanden
                var mot = new List<DataLayer.Models.Message>();

                //Om en parameterskickas med
                if (profileId != null)
                {
                    AddFriendVisible((int)profileId);

                    //Fyller tomma listan baserat på int parametern
                    mot = messages.Where(x => x.ReciverId == profileId).ToList();

                    //Uppdaterar user till den profil man tryckt på
                    user = _DatingContext.Profiles.SingleOrDefault(p => p.Id == profileId);
                }
                //Annars om en parameter inte skickas med
                else
                {
                    //Fyller tomma listan med egna meddelanden
                    mot = messages.Where(x => x.ReciverId == user.Id).ToList();
                }

                //Skapar ny profil för den som ska visas
                Profile profile = new Profile()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Age = user.Age,
                    Email = user.Email,
                    Gender = user.Gender,
                    SexualOrientation = user.SexualOrientation,
                    ImagePath = user.ImagePath
                };

                //Hämtar alla profiler i databas
                var profiles = _DatingContext.Profiles.ToList();

                //Ny tom lista
                List<Message> messages1 = new List<Message>();

                //Fyller tomma listan med nya meddelanden från databasen och ger dem en author genom
                //att jämföra SenderId med profilerna i profiles-listan
                foreach (var m in mot)
                {
                    Message message = new Message()
                    {
                        MessageId = m.MessageId,
                        SenderId = m.SenderId,
                        ReciverId = m.ReciverId,
                        Text = m.Text,
                        Date = m.Date,
                        Author = profiles.Single(x => x.Id == m.SenderId).Firstname
                    };
                    messages1.Add(message);
                };
                //Skapar en viewmodel och fyller den med profil och profilens meddelanden
                WallViewModel wallViewModel = new WallViewModel()
                {
                    Profile = profile,
                    WallMessages = messages1
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

            var listOfFriends = _DatingContext.FriendRequests.Where(x => x.FriendSenderId == id && x.FriendReciverId == user ||
             x.FriendReciverId == id && x.FriendSenderId == user).ToList();

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
            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id))
                .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

            //Om listan av vänförfrågningar är större än 0
            if (listatva.Count > 0)
            {
                ViewBag.Requests = true;
            }
        }

        // GET: Wall/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Wall/Create
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