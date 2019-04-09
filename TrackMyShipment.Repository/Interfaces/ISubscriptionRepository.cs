using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Supplies> GetSubscribe(int? userId, int? carrierId);
        Task<bool> Subscribe(int? carrierId, int? userId);
        Task<bool> DeleteSubscribe(Supplies relation);

    }
}
