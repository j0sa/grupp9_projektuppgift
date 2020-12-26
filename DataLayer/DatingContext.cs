﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataLayer
{
    public class DatingContext : DbContext
    {
        public DatingContext(DbContextOptions<DatingContext> options) : base(options)
        {
        }

            public DbSet<Profile> Profiles { get; set; }
        public DbSet<Friend> Friends { get; set; }

        
    }
}
