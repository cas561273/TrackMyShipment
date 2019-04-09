using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Manage
{
    public class CarrierManage
    {
        private readonly ICarrierService _carrierService;

        public CarrierManage(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public async Task<bool> AddOrUpdateCarrier(Carrier carrier)
        {
          return await _carrierService.AddOrUpdateCarrier(carrier);
        }

        public async Task<Carrier> GetByIdCarrier(int carrierId)
        {
            return await _carrierService.GetCarrierById(carrierId);
        }

        public async  Task<bool> DeleteCarrier(int id)
        {
            return await _carrierService.DeleteCarrier(id);
        }

        public async Task<IEnumerable<Carrier>> GetAvailableCarriers(User user)
        {
            return await _carrierService.GetAvailableCarriers(user);
        }

        public async Task<IEnumerable<Carrier>> GetMyCarriers(User user)
        {
            return await _carrierService.GetMyCarriers(user);
        }

        public async Task<bool?> ChangeStatusCarrier(int carrierId)
        {
            return  await _carrierService.ChangeStatusCarrier(carrierId);
        }
    }
}