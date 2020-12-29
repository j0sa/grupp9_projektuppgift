using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class FriendController : Controller
    {
        private readonly DatingContext _DatingContext;

        public FriendController(DatingContext datingContext)
        {
            _DatingContext = datingContext;
        }
        // GET: FriendRequestController
        [Authorize]
        public ActionResult Index()
        
        {
            //Hämtar inloggade användarens vänner via email och id
            var user = User.Identity.Name;
            var prf = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
            var id = prf.Id;


            var lista = _DatingContext.FriendRequests.Where(x => x.FriendSenderId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendReciverId).ToList();
            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendSenderId).ToList();

            var combinedList = lista.Concat(listatva).ToList();

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
        [Authorize]
        public ActionResult Requests()
        {

            //Hämtar inloggade användarens vänner via email och id
            var user = User.Identity.Name;
            var prf = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
            var id = prf.Id;

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

        [Authorize]
        [HttpPost]
        public ActionResult AcceptDecline(int FriendId, string AcceptDecline)
        {
            //Hämtar användarId
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            var UserId = user.Id;


            //Hittar rätt rad i Db för att kunna uppdatera den
                var friendFind = _DatingContext.FriendRequests.FirstOrDefault
                (x => x.FriendReciverId == UserId && x.FriendSenderId==FriendId);
            
            // Om ej null och Accept
            if (AcceptDecline=="Accept" && friendFind != null) { 

            
            

                friendFind.Accepted = true;

                _DatingContext.SaveChanges();

             
            }
            // Om ej null och Decline
            else if (AcceptDecline=="Decline" && friendFind != null)
            {
                _DatingContext.Remove(friendFind);
                _DatingContext.SaveChanges();
            }



                   
            return RedirectToAction(nameof(Requests));
        }




        [Authorize]
        [HttpPost]
        public ActionResult RemoveFriend(int FriendId)
        {
            //Hämtar användarId
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            var UserId = user.Id;


            //Hittar rätt rad i Db för att kunna uppdatera den
            var friendFind = _DatingContext.FriendRequests.FirstOrDefault
            (x => x.FriendReciverId == UserId && x.FriendSenderId == FriendId && x.Accepted==true);

            


            // Om ej null
            if (friendFind != null)
            {
                _DatingContext.Remove(friendFind);
                _DatingContext.SaveChanges();
            }




            return RedirectToAction(nameof(Index));
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
