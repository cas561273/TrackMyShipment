
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<IEnumerable<Address>> GetMyAddressAsync(int? userId);
        Task<Address> GetAddressByIdAsync(int? id);
        Task<Address> MyActiveAddressAsync(int? userId);

    }
}