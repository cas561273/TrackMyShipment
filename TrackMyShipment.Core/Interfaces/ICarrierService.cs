using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICarrierService
    {
        Task<IEnumerable<Carrier>> GetMyCarriers(User person);
        Task<IEnumerable<Carrier>> GetAvailableCarriers(User user);
        Task<Carrier> GetCarrierById(int carrierId);
        Task<bool> AddOrUpdateCarrier(Carrier carrier);
        Task<bool> DeleteCarrier(int id);
        Task<bool?> ChangeStatusCarrier(int carrierId);
    }
}