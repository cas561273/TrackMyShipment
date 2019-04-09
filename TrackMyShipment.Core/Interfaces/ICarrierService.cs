using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICarrierService
    {
        Task<IEnumerable<Carrier>> GetMyCarriersAsync(User person);
        Task<IEnumerable<Carrier>> GetAvailableCarriersAsync(User user);
        Task<Carrier> GetCarrierByIdAsync(int carrierId);
        Task<bool> AddOrUpdateCarrierAsync(Carrier carrier);
        Task<bool> DeleteCarrierAsync(int id);
        Task<bool?> ChangeStatusCarrierAsync(int carrierId);
    }
}