using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IAddressService
    {
        Task<bool?> DeleteAddress(int? id, int? userId);
        Task<bool?> StatusAddress(int? id, int? userId);
        Task<bool?> PutOrUpdate(Address address, int? userId);
        Task<IEnumerable<Address>> MyAddress(int? userId);
    }
}