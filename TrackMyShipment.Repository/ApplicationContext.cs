﻿using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Migrations;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Supplies>().HasKey(
                t => new { t.UserId, t.CarrierId }
            );

            builder.Seed();
        }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Supplies> Supplies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}