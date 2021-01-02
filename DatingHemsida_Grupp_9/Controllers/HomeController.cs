﻿using DataLayer;
using DatingHemsida_Grupp_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatingContext _datingContext;


        public HomeController(ILogger<HomeController> logger, DatingContext datingContext)
        {
            _logger = logger;
            _datingContext = datingContext;
            
            
        }
        
        public IActionResult Index()
        {

            var exampel = _datingContext.Profiles.Where(x=>x.ImagePath!="StandardProfil.png").ToList();

            

            var profiles = exampel.Select(p => new Profile
            {
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                Age = p.Age,
                Active = p.Active,
                Email = p.Email,
                SexualOrientation = p.SexualOrientation,
                ImagePath=p.ImagePath,

                UserPicture = p.UserPicture
            }).ToList();

            List<Profile> profiles1 = new List<Profile>();
            var prof1 = profiles.SingleOrDefault(x => x.Id == 21);
            profiles1.Add(prof1);
            var prof2 = profiles.SingleOrDefault(x => x.Id == 16);
            profiles1.Add(prof2);
            var prof3 = profiles.SingleOrDefault(x => x.Id == 2);
            profiles1.Add(prof3);








            //var visaExempel = exampel.Where(x=>x);

            //var personEntities = _datingContext.Profiles.ToList();

            //var persons = personEntities.Select(p => new Profile
            //{
            //    Firstname = p.Firstname,
            //    Lastname = p.Lastname,
            //    Gender = p.Gender
            //}).ToList();

            //foreach(Profile p in persons)
            //{
            //    Console.WriteLine(p.Firstname, p.Lastname, p.Gender);
            //}
            return View(profiles1);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
