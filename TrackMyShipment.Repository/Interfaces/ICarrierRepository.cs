using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Carrier GetByName(string name);
        IEnumerable<Carrier> GetCarriers(User user);
        IEnumerable<User> GetMyUser(int carrierId);
        Task<IEnumerable<Carrier>> GetAvailable(User user);
        Task<IEnumerable<Carrier>> GetFree();
        bool? Active(int carrierId);
    }
}