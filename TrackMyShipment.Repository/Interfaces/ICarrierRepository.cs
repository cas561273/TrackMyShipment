using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Task<IEnumerable<Carrier>> GetCarriersAsync(User user);
        Task<IEnumerable<Carrier>> GetAvailableCarriersAsync(User user);
        Task<bool?> ChangeStatusCarrierAsync(int carrierId);

    }
}