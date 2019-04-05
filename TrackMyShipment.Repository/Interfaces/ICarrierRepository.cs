using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Task<IEnumerable<Carrier>> GetCarriers(User user);
        Task<IEnumerable<User>> GetMyUsers(int carrierId);
        Task<IEnumerable<Carrier>> GetAvailable(User user);
        Task<bool?> ActiveStatus(int carrierId);
    }
}