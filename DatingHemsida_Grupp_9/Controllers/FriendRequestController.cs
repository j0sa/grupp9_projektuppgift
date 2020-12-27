using DataLayer;
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
            
    //        var accepted = _DatingContext.FriendRequests.Where(x => x.Accepted==true).ToList();

    //        //var friends = accepted.Where(x => x.FriendSenderId.Equals(1) 
    //        //|| x.FriendReciverId.Equals(1)).ToList();

            

    //        var radett = accepted.Select(x=>x.FriendSenderId).ToList();
    //        var radtva = accepted.Select(x => x.FriendReciverId).ToList();

    //        radett.AddRange(radtva);
    //        var profileEntities = _DatingContext.Profiles.Where(x=>x.Id).ToList();

    //        var filtered = _DatingContext.Profiles
    //               .Where(x => radett.Any(y => y == x.Id));

    //        IEnumerable<ItemBO> result = items.Where(item =>
    //categories.Any(category => category.ItemCategory.equals(item.ItemCategory)));

    //        //filteredLessons.Select(l => l.lessonId).ToList();

    //        //var profiles = _DatingContext.Profiles

    //        //    var mySkus = friends.Select(x => x.);



    //        var profiles = profileEntities.Select(p => new Profile
    //        {
    //            Id = p.Id,
    //            Firstname = p.Firstname,
    //            Lastname = p.Lastname,
    //            Gender = p.Gender,
    //            Age = p.Age,
    //            Active = p.Active,
    //            Email = p.Email,
    //            SexualOrientation = p.SexualOrientation,

    //            UserPicture = p.UserPicture
    //        }).ToList();
            return View();
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
