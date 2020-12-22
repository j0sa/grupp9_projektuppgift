using DataLayer.Models;
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
            //OnConfiguring(new DbContextOptionsBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //    optionsBuilder.UseSqlServer("Server=.;Database=DatingDb;Trusted_Connection=true;");
        //}

            public DbSet<User> Users { get; set; }

        
    }
}
