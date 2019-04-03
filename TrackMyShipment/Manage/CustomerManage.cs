using AutoMapper;
using System.Collections.Generic;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Manage
{
    public class CustomerManage
    {
        private readonly ICustomerService _customerService;

        public CustomerManage(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Address DeleteAddress(int? id, int? userId)
        {

            return _customerService.DeleteAddress(id, userId);

        }

        public string StatusAddress(int? id, int? userId)
        {

            return _customerService.StatusAddress(id, userId);
        }


        public string DeleteSubscribe(Supplies relation)
        {
            return _customerService.DeleteSubscribe(relation);
        }

        public Supplies GetSubscribe(int? userId, int? carrierId)
        {
            return _customerService.GetSubscribe(userId, carrierId);
        }

        public Address PutOrUpdate(AddressModel address, int? userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddressModel, Address>()).CreateMapper();
            Address addressModel = mapper.Map<AddressModel, Address>(address);

            return _customerService.PutOrUpdate(addressModel, userId);
        }

        public IEnumerable<AddressModel> MyAddress(int? userId)
        {
            var myAddress = _customerService.MyAddress(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Address>, IEnumerable<AddressModel>>(myAddress);
            
        }

        public string Subscribe(int? carrierId, int? userId)
        {
            return _customerService.Subscribe(carrierId, userId);
        }

    }
}

