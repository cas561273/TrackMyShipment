using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
         new Roles { Id = 1, Role = "admin" },
         new Roles { Id = 2, Role = "customer" },
         new Roles { Id = 3, Role = "carrier" });

            modelBuilder.Entity<Subscription>().HasData(
           new Subscription { Id = 1, Status = "free" },
           new Subscription { Id = 2, Status = "paid" });
        }
    }
}