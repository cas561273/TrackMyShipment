using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IAddressService
    {
        Task<bool?> DeleteAddressAsync(int? id, int? userId);
        Task<bool?> StatusAddressAsync(int? id, int? userId);
        Task<bool?> PutOrUpdateAsync(Address address, int? userId);
        Task<IEnumerable<Address>> MyAddressAsync(int? userId);
        Task<Address> MyActiveAddressAsync(int? userId);
        Task Complete();
    }
}