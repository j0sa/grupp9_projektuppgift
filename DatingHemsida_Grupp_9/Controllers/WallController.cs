using DataLayer;
using DatingHemsida_Grupp_9.Models;
using DatingHemsida_Grupp_9.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class WallController : Controller
    {
        private readonly DatingContext _DatingContext;

        public WallController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
            
        }

        public ActionResult Index()
        // GET: Wall
        {
            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);
            
            var messages = _DatingContext.Messages.ToList();
            var mot = messages.Where(x => x.ReciverId == 16).ToList();

            var profiles = _DatingContext.Profiles.ToList();

            Profile profile = new Profile()
            {
                Id=user.Id,
                Firstname=user.Firstname,
                Lastname=user.Lastname,
                Age=user.Age,
                Email=user.Email,
                Gender=user.Gender,
                SexualOrientation=user.SexualOrientation,
                ImagePath=user.ImagePath
            };

            List<Message> messages1 = new List<Message>()
            {
               

            };
            foreach (var m in mot) {
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
