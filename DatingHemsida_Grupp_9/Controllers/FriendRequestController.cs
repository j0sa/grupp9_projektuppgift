using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class FriendRequestController : Controller
    {
        private readonly DatingContext _DatingContext;

        public FriendRequestController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }
        // GET: FriendRequestController
        public ActionResult Index()
        {

            var lista = _DatingContext.FriendRequests.Where(x => x.FriendSenderId.Equals(3)).Where(x => x.Accepted == true).Select(x => x.FriendReciverId).ToList();
            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(3)).Where(x => x.Accepted == true).Select(x => x.FriendSenderId).ToList();



            var combinedList = lista.Concat(listatva).ToList();

            //var friends = _DatingContext.Profiles.Where(x=>x.Id );


            List<Profile> profileEntities = new List<Profile>();
            
            foreach (var i in combinedList)
            {
                
                var friend = _DatingContext.Profiles.SingleOrDefault(p => p.Id == i);
                
                profileEntities.Add(friend);
                }

            var profiles = profileEntities.Select(p => new  Models.Profile
            {
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                Age = p.Age,
                Active = p.Active,
                Email = p.Email,
                SexualOrientation = p.SexualOrientation,
                UserPicture = p.UserPicture
            }).ToList();





            return View(profiles);
        }

        // GET: FriendRequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FriendRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FriendRequestController/Create
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

        // GET: FriendRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FriendRequestController/Edit/5
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

        // GET: FriendRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FriendRequestController/Delete/5
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
