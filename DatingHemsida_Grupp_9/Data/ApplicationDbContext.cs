using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DatingHemsida_Grupp_9.Models;

namespace DatingHemsida_Grupp_9.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<DatingHemsida_Grupp_9.Models.User> User { get; set; }
    }
}
