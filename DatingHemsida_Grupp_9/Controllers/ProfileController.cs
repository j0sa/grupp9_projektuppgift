using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DatingContext _DatingContext;

        public ProfileController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }
        // GET: ProfileController
        public ActionResult Index()
        {
            var profileEntities = _DatingContext.Profiles.ToList();

            var profiles = profileEntities.Select(p => new Profile
            {
                Id=p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender
            }).ToList();

            return View(profiles);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
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

        // GET: ProfileController/Edit/5
        public ActionResult Edit(Profile profile)
        {
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Firstname == "Johannes");
            if (user == null)
            {
                //...
            }

            Profile profile1 = new Profile
            {
                Id=user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                UserPicture = user.UserPicture
            };
            return View(profile1);
        }

        // POST: ProfileController/Edit/5
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

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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
