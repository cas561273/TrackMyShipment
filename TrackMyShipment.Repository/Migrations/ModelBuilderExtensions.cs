using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Migrations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role {Id = 1, Name = "admin"},
                new Role {Id = 2, Name = "customer"},
                new Role {Id = 3, Name = "carrier"});

            modelBuilder.Entity<Subscription>().HasData(
                new Subscription {Id = 1, Status = "free"},
                new Subscription {Id = 2, Status = "paid"});

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Supplies>().HasKey(
                t => new { t.UserId, t.CarrierId }
            );
        }
    }
}