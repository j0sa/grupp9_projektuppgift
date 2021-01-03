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
    public class WallController : Controller
    {
        private readonly DatingContext _DatingContext;

        public WallController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }

        [Authorize]
        public ActionResult Index(int? profileId)
        // GET: Wall
        {
            
            

            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);
            
            if (UserName != null && user != null)
            { 
                FriendRequestVisible();

var messages = _DatingContext.Messages.ToList();

                var mot = new List<DataLayer.Models.Message>();

                if (profileId!=null) {

                    mot = messages.Where(x => x.ReciverId == profileId).ToList();
                    user = _DatingContext.Profiles.SingleOrDefault(p => p.Id == profileId);
                }
                else {mot = messages.Where(x => x.ReciverId == user.Id).ToList(); }
                
                
            

            var profiles = _DatingContext.Profiles.ToList();

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

            List<Message> messages1 = new List<Message>()
            {
            };
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

            WallViewModel wallViewModel = new WallViewModel()
            {
                Profile = profile,
                WallMessages = messages1
            };

            return View(wallViewModel);
            
            }
            //else if (UserName != null && user == null)
            //{
            //    return RedirectToAction("Create", "Profile");
            //}
            else { return RedirectToAction("Index", "Home"); }
        }







        [Authorize]
        public ActionResult GoToProfile(int ProfileId)
        // GET: Wall
        {



            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

            if (UserName != null && user != null)
            {

                FriendRequestVisible();

                var messages = _DatingContext.Messages.ToList();
                var mot = messages.Where(x => x.ReciverId == ProfileId).ToList();

                var profiles = _DatingContext.Profiles.ToList();
                var userr = _DatingContext.Profiles.SingleOrDefault(p => p.Id == ProfileId);

                Profile profile = new Profile()
                {
                    Id = userr.Id,
                    Firstname = userr.Firstname,
                    Lastname = userr.Lastname,
                    Age = userr.Age,
                    Email = userr.Email,
                    Gender = userr.Gender,
                    SexualOrientation = userr.SexualOrientation,
                    ImagePath = userr.ImagePath
                };

                List<Message> messages1 = new List<Message>()
                {
                };
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

                WallViewModel wallViewModel = new WallViewModel()
                {
                    Profile = profile,
                    WallMessages = messages1
                };

                return View(wallViewModel);

            }
            
            else { return RedirectToAction("Index", "Home"); }
        }







        //Kontrollerar navbar
        public void FriendRequestVisible()
        {
            //Skickar true or false till vyn för att visa knappen friendrequests om det finns några
            ViewBag.Requests = false;
            var user = User.Identity.Name;

            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
            
                
                var id = profile.Id;

                var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id))
                    .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

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