using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
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

        

        [HttpGet]
        public ActionResult Index()

        {
           
            FriendRequestVisible();

            //Hämtar inloggade användarens vänner via email
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(User.Identity.Name));
            
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var id = profile.Id;
            /*
            Skapar två listor, en bestående av profiler där vänförfrågningar som den inloggade skickat är 
            accepterade och en av profiler där den inloggade tagit emot och accepterat vänförfrågningar.
            Sedan sätts dessa två listor ihop för att skapa en stor vänlista
             */
            var senderList = _DatingContext.FriendRequests.Where(x => x.FriendSenderId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendReciverId).ToList();
            var receiverList = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id)).Where(x => x.Accepted == true).Select(x => x.FriendSenderId).ToList();

            var combinedList = senderList.Concat(receiverList).ToList();

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

        [HttpGet]
        public ActionResult Requests()
        {
            
            FriendRequestVisible();

            //Hämtar inloggade användarens vänner via email
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(User.Identity.Name));

            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var id = profile.Id;

            //Lista av vänförfrågningar
            var receiverList = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id)).Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

            List<Profile> profileEntities = new List<Profile>();

            foreach (var i in receiverList)
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
           
            var UserId = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name).Id;
            
            //Hittar rätt rad i Db för att kunna uppdatera den
            var friendFound = _DatingContext.FriendRequests.FirstOrDefault
            (x => x.FriendReciverId == UserId && x.FriendSenderId == FriendId);

            // Om ej null och Accept
            if (AcceptDecline == "Accept" && friendFound != null)
            {
                friendFound.Accepted = true;
                _DatingContext.SaveChanges();
            }
            // Om ej null och Decline
            else if (AcceptDecline == "Decline" && friendFound != null)
            {
                _DatingContext.Remove(friendFound);
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

            return RedirectToAction("Index", "Wall", new { profileId = reciverId }); ;
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

            if (user != null && profile != null)
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
    }
}