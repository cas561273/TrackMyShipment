using System.Collections.Generic;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICarrierService
    {
        IEnumerable<Carrier> GetMyCarriers(User person);
        Carrier Get(Carrier carrier);
        IEnumerable<Carrier> GetAll();
        void AddOrUpdate(Carrier carrier);
        bool Delete(int id);
        IEnumerable<Carrier> GetAvailable(User user);
        Carrier GetById(int carrierId);
        IEnumerable<User> GetMyUsers(int carrierId);
        bool Active(int carrierId);

    }
}
