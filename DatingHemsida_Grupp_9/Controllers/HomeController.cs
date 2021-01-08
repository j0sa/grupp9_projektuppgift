using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatingContext _DatingContext;

        public HomeController(ILogger<HomeController> logger, DatingContext datingContext)
        {
            _logger = logger;
            _DatingContext = datingContext;
        }

        public IActionResult Index()
        {
           
                //Randomiserar profiler som sedan skickas vidare för att visas som exempelprofiler på startsidan
                Random random = new Random();
                int count = random.Next(0, _DatingContext.Profiles.Where(x => x.Active == true).Count());
                var profileEntities = new List<DataLayer.Models.Profile>();

                int userId;
                var userName = User.Identity.Name;
                var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(userName));

                if (User.Identity.IsAuthenticated && profile!=null)
                {
                    
                    userId = _DatingContext.Profiles.SingleOrDefault(p => p.Email == userName).Id;
                }
                else
                {
                    userId = 0;  
                }

                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Id != userId).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

                //Skickar false till vyn för att knappen friendrequests först ej ska synas
                ViewBag.Requests = false;

                if (userName != null && profile != null)
                {
                    FriendRequestVisible();
                }
                //Om inloggad men ej skapat profil
                else if (userName != null && profile == null)
                {
                    return RedirectToAction("Create", "Profile");
                }

                var profiles = profileEntities.Select(p => new Profile
                {
                    Id = p.Id,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Gender = p.Gender,
                    Age = p.Age,
                    Active = p.Active,
                    Email = p.Email,
                    SexualOrientation = p.SexualOrientation,
                    ImagePath = p.ImagePath,

                    UserPicture = p.UserPicture
                }).ToList();

                return View(profiles);
        }

        public void FriendRequestVisible()
        {
            //Skickar true or false till vyn för att visa knappen friendrequests om det finns några
            ViewBag.Requests = false;
            

            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(User.Identity.Name));

            var id = profile.Id;

            var listatva = _DatingContext.FriendRequests.Where(x => x.FriendReciverId.Equals(id))
                .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();

            if (listatva.Count > 0)
            {
                ViewBag.Requests = true;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}