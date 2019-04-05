using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Manage
{
    public class CarrierManage
    {
        private readonly ICarrierService _carrierService;

        public CarrierManage(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public void AddOrUpdate(Carrier carrier)
        {
            _carrierService.AddOrUpdate(carrier);
        }

        public async Task<Carrier> GetById(int carrierId)
        {
            return await _carrierService.GetById(carrierId);
        }

        public async  Task<bool> Delete(int id)
        {
            return await _carrierService.Delete(id);
        }


        public async Task<IEnumerable<Carrier>> GetAvailable(User user)
        {
            return await _carrierService.GetAvailable(user);
        }

        public async Task<IEnumerable<Carrier>> GetMyCarriers(User user)
        {
            return await _carrierService.GetMyCarriers(user);
        }

        public async Task<IEnumerable<UserModel>> GetMyUsers(int carrierId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()).CreateMapper();
            var people = await _carrierService.GetMyUsers(carrierId);
            IEnumerable<UserModel> peopleShort =  mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(people);
            return  peopleShort;
        }

        public async Task<bool?> ActiveStatus(int carrierId)
        {
            return  await _carrierService.ActiveStatus(carrierId);
        }
    }
}