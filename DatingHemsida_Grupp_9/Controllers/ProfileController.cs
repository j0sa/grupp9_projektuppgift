using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly DatingContext _DatingContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(DatingContext datingContext, IWebHostEnvironment hostEnvironment)
        {
            _DatingContext = datingContext;
            this._hostEnvironment = hostEnvironment;
        }

        /*
         * Sökfunktionen
         * Kan söka på gender eller efter för-/efternamn
         * Om inget anges så visas endast alla användare som satt sin profil som aktiv
         */

        public ActionResult Index(string searchString, string searchGender)
        {
            ViewBag.NoResult = "";
            FriendRequestVisible();
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(User.Identity.Name));
            //Om ej skapat profil - skickas tillbaka till startsidan som sedan skickar vidare till skapa profil
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var profileEntities = new List<DataLayer.Models.Profile>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchGender))
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Firstname.Contains(searchString) && x.Gender.Equals(searchGender)
                    || x.Active == true && x.Lastname.Contains(searchString) && x.Gender.Equals(searchGender)).OrderBy(x => x.Firstname).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && searchGender == "")
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Firstname.Contains(searchString)
                    || x.Active == true && x.Lastname.Contains(searchString)).OrderBy(x => x.Firstname).ToList();
            }
            else if (String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchGender))
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Gender.Equals(searchGender))
                    .OrderBy(x => x.Firstname).ToList();
            }
            else if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(searchGender))
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true && x.Firstname.Contains(searchString) || x.Active == true &&
                    x.Active == true && x.Lastname.Contains(searchString)).OrderBy(x => x.Firstname).ToList();
            }
            else
            {
                profileEntities = _DatingContext.Profiles.Where(x => x.Active == true).ToList();
            }
            if (profileEntities.Count() == 0)
            {
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
                SexualOrientation = p.SexualOrientation
            }).ToList();
            return View(profiles);
        }

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

        /*
         * Skapar en ny profil och binder den till en bild.
         * Om ingen bild anges, används en standardbild.
         * Bilden sparas i wwwroot och vägen till bilden sparas hos profilen i databasen.
         * Användaren matar in allt förrutom sin email och active vid detta tillfälle då email används för att koppla den egna databasen med inloggningsdatabasen.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id, Firstname, Lastname, Age, Email, Gender, SexualOrientation, Active, ImageFile")] Profile profile)
        {
            if (ModelState.IsValid)
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
                        Active = true,
                        Age = profile.Age,
                        Email = User.Identity.Name.ToString(),
                        SexualOrientation = profile.SexualOrientation,
                        ImagePath = profile.ImagePath
                    });
                    await _DatingContext.SaveChangesAsync();
                    return RedirectToAction("index", "Wall");
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }
        }

        
        public ActionResult Edit()
        {
            //Kontrollerar navbar
            FriendRequestVisible();
            var user = _DatingContext.Profiles.SingleOrDefault(p => p.Email == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Profile profile = new Profile
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = user.Gender,
                Active = user.Active,
                Age = user.Age,
                Email = user.Email,
                SexualOrientation = user.SexualOrientation,
                ImagePath = user.ImagePath
            };
            TempData["img"] = user.ImagePath;
            return View(profile);
        }

        /*Uppdaterar profilen och binder på samma sätt som i create.
         * Användaren kan ändra vad denne önskar och kan lämna bild tom då den sparade bilden inte uppdateras om en ny bild inte skickas in.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id, Firstname, Lastname, Age, Email, Gender, SexualOrientation, Active, ImageFile")] Profile profile)
        {
            if (ModelState.IsValid)
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
                            Active = profile.Active,
                            Age = profile.Age,
                            Email = profile.Email,
                            SexualOrientation = profile.SexualOrientation,
                            ImagePath = TempData["img"] as string
                        });
                        _DatingContext.SaveChanges();
                    }
                    return Redirect("/Identity/Account/Manage/ChangePassword");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        //Skickar true or false till vyn för att visa knappen friendrequests om det finns några
        public void FriendRequestVisible()
        {
            ViewBag.Requests = false;
            var profile = _DatingContext.Profiles.SingleOrDefault(p => p.Email.Equals(User.Identity.Name));
            if (User.Identity.Name != null && profile != null)
            {
                var listNotAccepted = _DatingContext.FriendRequests.Where(x => x.FriendReceiverId.Equals(profile.Id))
                    .Where(x => x.Accepted == false).Select(x => x.FriendSenderId).ToList();
                if (listNotAccepted.Count > 0)
                {
                    ViewBag.Requests = true;
                }
            }
        }
    }
}