using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<bool> Subscribe(int? carrierId, int? userId);
        Task<Supplies> GetSubscribe(int? userId, int? carrierId);
        Task<bool> DeleteSubscribe(Supplies relation);

    }
}
