using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DatingHemsida_Grupp_9.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly DatingContext _DatingContext;

        public FriendController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }

        // GET: FriendRequestController
        
        public ActionResult Index()

        {
            //Kontrollerar navbar
            FriendRequestVisible();

            //Hämtar inloggade användarens vänner via email och id
            var user = User.Identity.Name;
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var id = profile.Id;

            var lista = _DatingContext.FriendRequests.Where(x => x.FriendSenderId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendReciverId).ToList();
            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendSenderId).ToList();

            var combinedList = lista.Concat(listatva).ToList();

            List<Profile> profileEntities = new List<Profile>();

            foreach (var i in combinedList)
            {
                var friend = _DatingContext.Profiles.SingleOrDefault(p => p.Id == i);

                profileEntities.Add(friend);
            }

            var profiles = profileEntities.Select(p => new Models.Profile
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
        public ActionResult Requests()
        {
            //Kontrollerar navbar
            FriendRequestVisible();

            //Hämtar inloggade användarens vänner via email och id
            var user = User.Identity.Name;
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));

            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var id = profile.Id;

            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id)).Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

            List<Profile> profileEntities = new List<Profile>();

            foreach (var i in listatva)
            {
                var friend = _DatingContext.Profiles.SingleOrDefault(p => p.Id == i);

                profileEntities.Add(friend);
            }

            var profiles = profileEntities.Select(p => new Models.Profile
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

       
        [HttpPost]
        public ActionResult AcceptDecline(int FriendId, string AcceptDecline)
        {
            //Hämtar användarId
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            var UserId = user.Id;

            //Hittar rätt rad i Db för att kunna uppdatera den
            var friendFind = _DatingContext.FriendRequests.FirstOrDefault
            (x => x.FriendReciverId == UserId && x.FriendSenderId == FriendId);

            // Om ej null och Accept
            if (AcceptDecline == "Accept" && friendFind != null)
            {
                friendFind.Accepted = true;

                _DatingContext.SaveChanges();
            }
            // Om ej null och Decline
            else if (AcceptDecline == "Decline" && friendFind != null)
            {
                _DatingContext.Remove(friendFind);
                _DatingContext.SaveChanges();
            }

            return RedirectToAction(nameof(Requests));
        }

        [HttpPost]
        public ActionResult AddFriend(int reciverId)
        {

            var UserName = User.Identity.Name;
            int user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName).Id;

            DataLayer.Models.FriendRequest friendRequest = new DataLayer.Models.FriendRequest()
            {
                FriendSenderId = user,
                FriendReciverId = reciverId,
                Accepted = false,

            };
            _DatingContext.FriendRequests.Add(friendRequest);
            _DatingContext.SaveChanges();

            return RedirectToAction("Index", "Wall", new {profileId=reciverId}); ;

        }

        [HttpPost]
        public ActionResult RemoveFriend(int FriendId)
        {
            //Hämtar användarId
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            var UserId = user.Id;

            //Hittar rätt rad i Db för att kunna uppdatera den
            var friendFind = _DatingContext.FriendRequests.FirstOrDefault
            (x => x.FriendReciverId == UserId && x.FriendSenderId == FriendId && x.Accepted == true);

            // Om ej null
            if (friendFind != null)
            {
                _DatingContext.Remove(friendFind);
                _DatingContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public void FriendRequestVisible()
        {
            //Skickar true or false till vyn för att visa knappen friendrequests om det finns några
            ViewBag.Requests = false;
            var user = User.Identity.Name;
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
                
            if (user != null&&profile!=null)
            {
                var id = profile.Id;

                var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id))
                    .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

                if (listatva.Count > 0)
                {
                    ViewBag.Requests = true;
                }
            }
        }

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