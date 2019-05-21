using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Manage
{
    public class AddressManage
    {
        private readonly IAddressService _addressService;

        public AddressManage(IAddressService addressService)
        {
            _addressService = addressService;
        }


        public async Task<bool?> DeleteAddress(int? id, int? userId)
        {
            return await _addressService.DeleteAddressAsync(id, userId);
        }

        public async Task<bool?> ChangeStatusAddress(int? id, int? userId)
        {
            return await _addressService.StatusAddressAsync(id, userId);
        }
        public async Task<bool?> PutOrUpdateAddress(AddressModel address, int? userId)
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddressModel, Address>()).CreateMapper();
            Address addressModel = mapper.Map<AddressModel, Address>(address);
            return await _addressService.PutOrUpdateAsync(addressModel, userId);
        }

        public async Task<IEnumerable<AddressModel>> MyAddress(int? userId)
        {
            IEnumerable<Address> myAddress = await _addressService.MyAddressAsync(userId);
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Address>, IEnumerable<AddressModel>>(myAddress);
        }

        public async Task<AddressModel> MyActiveAddress(User user)
        {
            Address myAddress = await _addressService.MyActiveAddressAsync(user.Id);
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressModel>()).CreateMapper();
            AddressModel addressModel =  mapper.Map<Address, AddressModel>(myAddress);
            addressModel.CompanyName = user.Company.Name;
            await _addressService.Complete();
            return addressModel;
        }
    }

}
