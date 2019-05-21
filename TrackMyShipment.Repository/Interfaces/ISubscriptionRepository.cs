using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ISubscriptionRepository:IRepository<Subscription>
    {
        Task<Supplies> GetSubscribeAsync(int? userId, int? carrierId);
        Task<bool> SubscribeAsync(int? carrierId, int? userId);
        Task<bool> DeleteSubscribeAsync(Supplies relation);

    }
}
