using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
   public class AddressService:IAddressService
    {
        private readonly IAddressRepository _context;

        public AddressService(IAddressRepository context)
        {
            _context = context;
        }

        public async Task<bool?> DeleteAddress(int? id, int? userId)
        {
            var address = await _context.GetAddressById(id);
            if (address.UsersId != userId) return null;
            _context.Remove(address);
            await _context.CompleteAsync();
            return true;
        }

        public async Task<bool?> StatusAddress(int? id, int? userId)
        {
            var address = await _context.GetAddressById(id);
            if (address.UsersId != userId) return null;
            address.Active = !address.Active;
            await _context.CompleteAsync();
            return true;
        }

        public async Task<bool?> PutOrUpdate(Address address, int? userId)
        {
            if (address == null) return null;

            Address existedAddress = await _context.GetAddressById(address.Id);

            if (existedAddress != null)
                existedAddress.CopyPropertyValues(address);   

            else
            {
                address.UsersId = userId;
                 _context.Add(address);
            }
            await _context.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Address>> MyAddress(int? userId)
        {
            return await _context.GetMyAddress(userId);
        }

    }
}