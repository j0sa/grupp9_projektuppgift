using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DatingContext _DatingContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(DatingContext datingContext, IWebHostEnvironment hostEnvironment)
        {
            _DatingContext = datingContext;
            this._hostEnvironment = hostEnvironment;
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
                Gender = p.Gender,
                Age=p.Age,
                Active=p.Active,
                Email=p.Email,
                SexualOrientation=p.SexualOrientation,
                
                UserPicture = p.UserPicture
            }).ToList();

            return View(profiles);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details()
        {
                var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);
            
            if (user == null)
            {
               // ...
            }

            Profile profile1 = new Profile
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                UserPicture = user.UserPicture,
                ImagePath=user.ImagePath
            };
            return View(profile1);
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id, Firstname, Lastname, Age, Email, Gender, SexualOrientation, Active, ImageFile")]Profile profile)
        {
            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);
                string extension = Path.GetExtension(profile.ImageFile.FileName);
                profile.ImagePath = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {

                    profile.ImageFile.CopyToAsync(fileStream);

                }
                   
                    _DatingContext.Profiles.Add(new DataLayer.Models.Profile
                    {

                        Firstname = profile.Firstname,
                        Lastname = profile.Lastname,
                        Gender = profile.Gender,
                        UserPicture = profile.UserPicture,
                        Active = true,
                        Age = profile.Age,
                        Email = User.Identity.Name.ToString(),
                        SexualOrientation = profile.SexualOrientation,
                        ImagePath=profile.ImagePath

                    });
                _DatingContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit()
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
                UserPicture = user.UserPicture,
                Active=user.Active,
                Age=user.Age,
                Email=user.Email,
                SexualOrientation=user.SexualOrientation,
                ImagePath=user.ImagePath
            };
            return View(profile1);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profile profile)
        {
            try
            {
                //profile = new Profile
                 _DatingContext.Profiles.Update(new DataLayer.Models.Profile
                 {
                    Id = profile.Id,
                    Firstname = profile.Firstname,
                    Lastname = profile.Lastname,
                    Gender = profile.Gender,
                    UserPicture = profile.UserPicture,
                    Active=profile.Active,
                    Age=profile.Age,
                    Email=profile.Email,
                    SexualOrientation=profile.SexualOrientation,
                    ImagePath = profile.ImagePath
                    
                    
                });
                _DatingContext.SaveChanges();

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
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Firstname == "Johannes");
            //UserName = User.Identity.Name
            if (user == null)
            {
                //...
            }
            

            Profile profile1 = new Profile
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                UserPicture = user.UserPicture
            };
            return View(profile1);
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
