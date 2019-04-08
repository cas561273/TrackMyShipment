using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
   public interface ISubscriptionService
    {
        Task<bool> Subscribe(Carrier carrier, User user);
        Task<bool> DeleteSubscribe(Supplies relation);
        Task<Supplies> GetSubscribe(int? userId, int? carrierId);
    }
}
