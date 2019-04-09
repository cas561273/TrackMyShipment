﻿using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
   public interface ISubscriptionService
    {
        Task<Supplies> GetSubscribeAsync(int? userId, int? carrierId);
        Task<bool> SubscribeAsync(Carrier carrier, User user);
    }
}