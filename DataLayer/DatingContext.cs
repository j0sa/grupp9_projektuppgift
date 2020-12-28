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
        }

            public DbSet<Profile> Profiles { get; set; }
       
        public DbSet<Message> Messages { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Messages
            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Reciver)
                        .WithMany(t => t.RecivedMessages)
                        .HasForeignKey(m => m.ReciverId)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Sender)
                        .WithMany(t => t.SentMessages)
                        .HasForeignKey(m => m.SenderId)
                        .OnDelete(DeleteBehavior.Restrict);

            //FriendRequests
            modelBuilder.Entity<FriendRequest>()
                       .HasOne(m => m.FriendSender)
                       .WithMany(t => t.SentFriendRequests)
                       .HasForeignKey(m => m.FriendSenderId)
                       .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<FriendRequest>()
                        .HasOne(m => m.FriendReciver)
                        .WithMany(t => t.RecivedFriendRequests)
                        .HasForeignKey(m => m.FriendReciverId)
                        .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
