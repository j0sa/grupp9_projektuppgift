using DataLayer;
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
            var personEntities = _datingContext.Profiles.ToList();

            var persons = personEntities.Select(p => new Profile
            {
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender
            }).ToList();

            foreach(Profile p in persons)
            {
                Console.WriteLine(p.Firstname, p.Lastname, p.Gender);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
