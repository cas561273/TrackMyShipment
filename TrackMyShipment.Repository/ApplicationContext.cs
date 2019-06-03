using Microsoft.EntityFrameworkCore;
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
            builder.Seed();
        }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Supplies> Supplies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Objective> Task { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
    }
}