
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetByAddress(int? id);
    }
}