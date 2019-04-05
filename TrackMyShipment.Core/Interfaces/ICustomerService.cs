using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Address> PutOrUpdate(Address address, int? userId);
        Task<string> Subscribe(int? carrierId, int? userId);
        Task<Address> DeleteAddress(int? id, int? userId);
        Task<string> StatusAddress(int? id, int? userId);
        Task<string> DeleteSubscribe(Supplies relation);
        Task<Supplies> GetSubscribe(int? userId, int? carrierId);
        Task<IEnumerable<Address>> MyAddress(int? userId);
    }
}