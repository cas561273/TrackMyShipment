using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICarrierService
    {
        IEnumerable<Carrier> GetMyCarriers(User person);
        IEnumerable<Carrier> GetAll();
        void AddOrUpdate(Carrier carrier);
        bool Delete(int id);
        Task<IEnumerable<Carrier>> GetAvailable(User user);
        Carrier GetById(int carrierId);
        IEnumerable<User> GetMyUsers(int carrierId);
        bool? ActiveStatus(int carrierId);
    }
}