using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<Address> DeleteAddress(int? id, int? userId)
        {
            return await _customerService.DeleteAddress(id, userId);
        }

        public async Task<string> StatusAddress(int? id, int? userId)
        {
            return await _customerService.StatusAddress(id, userId);
        }

        public async Task<string> DeleteSubscribe(Supplies relation)
        {
            return await _customerService.DeleteSubscribe(relation);
        }

        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return await _customerService.GetSubscribe(userId, carrierId);
        }

        public async Task<Address> PutOrUpdate(AddressModel address, int? userId)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddressModel, Address>()).CreateMapper();
            Address addressModel = mapper.Map<AddressModel, Address>(address);

            return await _customerService.PutOrUpdate(addressModel, userId);
        }

        public async Task<IEnumerable<AddressModel>> MyAddress(int? userId)
        {
            IEnumerable<Address> myAddress = await _customerService.MyAddress(userId);
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Address>, IEnumerable<AddressModel>>(myAddress);
        }

        public async Task<string> Subscribe(int? carrierId, int? userId)
        {
            return await _customerService.Subscribe(carrierId, userId);
        }
    }
}