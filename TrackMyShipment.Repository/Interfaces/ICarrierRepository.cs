using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        IEnumerable<Carrier> GetCarriers(User user);
        IEnumerable<User> GetMyUser(int carrierId);
        Task<IEnumerable<Carrier>> GetAvailable(User user);
        bool? ActiveStatus(int carrierId);
    }
}