using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Index(string searchString, string searchGender)
        {
            ViewBag.NoResult = "";
            //Kontrollerar navbar
            FriendRequestVisible();
            var profileEntities = new List<DataLayer.Models.Profile>();

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchGender))
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active==true && x.Firstname.Contains(searchString) && x.Gender.Equals(searchGender)
                || x.Active == true && x.Lastname.Contains(searchString) && x.Gender.Equals(searchGender)).OrderBy(x => x.Firstname).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString)&&searchGender=="")
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Firstname.Contains(searchString)
               || x.Active == true && x.Lastname.Contains(searchString)).OrderBy(x => x.Firstname).ToList();

            }
            else if (String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchGender))
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Gender.Equals(searchGender)).OrderBy(x => x.Firstname).ToList();

            }
            else if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(searchGender)) {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Firstname.Contains(searchString)|| x.Active == true &&
                x.Active == true && x.Lastname.Contains(searchString)).OrderBy(x => x.Firstname).ToList();


            }

            else { profileEntities = _DatingContext.Profiles.Where(x=>x.Active==true).ToList(); }

            

            if (profileEntities.Count()==0) {
                ViewBag.NoResult = "No result!";
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

                UserPicture = p.UserPicture
            }).ToList();

            return View(profiles);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details()
        {
            //Kontrollerar navbar
            FriendRequestVisible();

            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

            Profile profile1 = new Profile
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                UserPicture = user.UserPicture,
                Age = user.Age,
                SexualOrientation = user.SexualOrientation,

                ImagePath = user.ImagePath
            };
            return View(profile1);
        }

        // GET: ProfileController/Create
        [Authorize]
        public ActionResult Create()
        {
            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id, Firstname, Lastname, Age, Email, Gender, SexualOrientation, Active, ImageFile")] Profile profile)
        {
            try
            {
               

                var pic = profile.ImageFile;
                if (pic == null)
                {
                    profile.ImagePath = "StandardProfil.png";
                }
                else
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);
                    string extension = Path.GetExtension(profile.ImageFile.FileName);
                    profile.ImagePath = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await profile.ImageFile.CopyToAsync(fileStream);
                    }
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
                    ImagePath = profile.ImagePath
                });
                await _DatingContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit()
        {
            //Kontrollerar navbar
            FriendRequestVisible();

            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

            Profile profile1 = new Profile
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                UserPicture = user.UserPicture,
                Active = user.Active,
                Age = user.Age,
                Email = user.Email,
                SexualOrientation = user.SexualOrientation,
                ImagePath = user.ImagePath
            };
            TempData["img"] = user.ImagePath;
            return View(profile1);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id, Firstname, Lastname, Age, Email, Gender, SexualOrientation, Active, ImageFile")] Profile profile)
        {
            try
            {
                //Uppdaterar hela profilen inklusive bild
                if (profile.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);
                    string extension = Path.GetExtension(profile.ImageFile.FileName);
                    profile.ImagePath = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await profile.ImageFile.CopyToAsync(fileStream);
                    }

                    _DatingContext.Profiles.Update(new DataLayer.Models.Profile
                    {
                        Id = profile.Id,
                        Firstname = profile.Firstname,
                        Lastname = profile.Lastname,
                        Gender = profile.Gender,
                        UserPicture = profile.UserPicture,
                        Active = profile.Active,
                        Age = profile.Age,
                        Email = User.Identity.Name.ToString(),
                        SexualOrientation = profile.SexualOrientation,
                        ImagePath = profile.ImagePath
                    });
                    await _DatingContext.SaveChangesAsync();
                }
                //Uppdaterar profil men behåller tidigare bild om ingen ny laddats upp
                else
                {
                    _DatingContext.Profiles.Update(new DataLayer.Models.Profile
                    {
                        Id = profile.Id,
                        Firstname = profile.Firstname,
                        Lastname = profile.Lastname,
                        Gender = profile.Gender,
                        UserPicture = profile.UserPicture,
                        Active = profile.Active,
                        Age = profile.Age,
                        Email = profile.Email,
                        SexualOrientation = profile.SexualOrientation,
                        ImagePath = TempData["img"] as string
                    }); ; ;
                    _DatingContext.SaveChanges();
                }
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
            var UserName = User.Identity.Name;
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == UserName);

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

        public void FriendRequestVisible()
        {
            //Skickar true or false till vyn för att visa knappen friendrequests om det finns några
            ViewBag.Requests = false;
            var user = User.Identity.Name;

            if (user != null)
            {
                var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(user));
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