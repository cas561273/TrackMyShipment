using AutoMapper;
using System.Collections.Generic;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Manage
{
    public class CarrierManage
    {

        public readonly ICarrierService _carrierService;
        public CarrierManage(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public void AddOrUpdate(Carrier carrier)
        {
           _carrierService.AddOrUpdate(carrier);
        }

        public Carrier Get(Carrier carrier)
        {
            return _carrierService.Get(carrier);
        }

        public Carrier GetById(int carrierId)
        {
            return _carrierService.GetById(carrierId);
        }
        public bool Delete(int id)
        {
           return  _carrierService.Delete(id);
        }
        public IEnumerable<Carrier> GetAll()
        {
            return _carrierService.GetAll();
        }

        public IEnumerable<Carrier> GetAvailable(User user)
        {

            return _carrierService.GetAvailable(user);

        }

        public IEnumerable<Carrier> GetMyCarriers(User user)
        {
            return _carrierService.GetMyCarriers(user);
        }

        public IEnumerable<UserModel> GetMyUsers(int carrierId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()).CreateMapper();


            IEnumerable<User> people = _carrierService.GetMyUsers(carrierId);
            IEnumerable<UserModel> peopleShort;
            peopleShort = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(people);

            return peopleShort;
        }

        public bool Active(int carrierId)
        {
            return _carrierService.Active(carrierId);
        }
    }
}
