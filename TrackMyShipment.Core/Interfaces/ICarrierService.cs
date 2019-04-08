using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICarrierService
    {
        Task<IEnumerable<Carrier>> GetMyCarriers(User person);
        Task<bool> AddOrUpdate(Carrier carrier);
        Task<bool> Delete(int id);
        Task<IEnumerable<Carrier>> GetAvailable(User user);
        Task<Carrier> GetById(int carrierId);
        Task<IEnumerable<User>> GetMyUsers(int carrierId);
        Task<bool?> ActiveStatus(int carrierId);
    }
}