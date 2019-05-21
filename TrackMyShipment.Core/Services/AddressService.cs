using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _context;

        public AddressService(IAddressRepository context)
        {
            _context = context;
        }

        public async Task<bool?> DeleteAddressAsync(int? id, int? userId)
        {
            Address address = await _context.GetAddressByIdAsync(id);
            if (address.UsersId != userId)
            {
                return null;
            }

            _context.Remove(address);
            await _context.CompleteAsync();
            return true;
        }

        public async Task<bool?> StatusAddressAsync(int? id, int? userId)
        {
            Address address = await _context.GetAddressByIdAsync(id);
            if (address.UsersId != userId)
            {
                return null;
            }

            address.Active = !address.Active;
            await _context.CompleteAsync();
            return true;
        }

        public async Task<bool?> PutOrUpdateAsync(Address address, int? userId)
        {
            if (address == null)
            {
                return null;
            }

            Address existedAddress = await _context.GetAddressByIdAsync(address.Id);

            if (existedAddress != null)
            {
                existedAddress.UsersId = userId;
                existedAddress.Active = true;
                existedAddress.City = address.City;
                existedAddress.State = address.State;
                existedAddress.StreetLine1 = address.StreetLine1;
                existedAddress.StreetLine2 = address.StreetLine2;
                existedAddress.ZipCode = address.ZipCode;
            }
            else
            {
                address.UsersId = userId;
                _context.Add(address);
            }

            await _context.CompleteAsync();
            return true;
        }

        public async Task<Address> MyActiveAddressAsync(int? userId)
        {
            return await _context.MyActiveAddressAsync(userId);
        }

        public async Task<IEnumerable<Address>> MyAddressAsync(int? userId)
        {
            return await _context.GetMyAddressAsync(userId);
        }

        public async Task Complete()
        {
            await _context.CompleteAsync();
        }
    }
}