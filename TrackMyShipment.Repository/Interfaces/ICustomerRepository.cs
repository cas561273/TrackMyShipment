using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Address>
    {
        Task<bool> Subscribe(int? carrierId, int? userId);
        Task<Address> GetByAddress(int? id);
        Task<Supplies> GetSubscribe(int? userId, int? carrierId);
        Task<bool> DeleteSubscribe(Supplies relation);
    }
}