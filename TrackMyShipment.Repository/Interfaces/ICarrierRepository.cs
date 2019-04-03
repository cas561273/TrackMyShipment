using System.Collections.Generic;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Carrier GetByName(string name);
        IEnumerable<Carrier> GetCarriers(User user);
        IEnumerable<User> GetMyUser(int carrierId);
        IEnumerable<Carrier> GetAvailable(User user);
        IEnumerable<Carrier> GetFree();
        bool Active(int carrierId);



    }
}
