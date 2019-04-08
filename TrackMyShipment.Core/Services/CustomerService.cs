using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _context;

        public CustomerService(ICustomerRepository context)
        {
            _context = context;
        }

        public async Task<Address> DeleteAddress(int? id, int? userId)
        {
            var address = await _context.GetByAddress(id);
            if (address.UsersId != userId) return null;
            _context.Remove(address);
            await _context.CompleteAsync();
            return address;
        }

        public async Task<string> StatusAddress(int? id, int? userId)
        {
            var address = await _context.GetByAddress(id);
            if (address.UsersId != userId) return null;
            address.Active = !address.Active;
            await _context.CompleteAsync();
            return "Address successfully deleted";
        }

        public async Task<string> DeleteSubscribe(Supplies relation)
        {
            var result = await _context.DeleteSubscribe(relation);
            return result ? "Delete Successfully" : null;
        }

        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return await _context.GetSubscribe(userId, carrierId);
        }

        public async Task<Address> PutOrUpdate(Address address, int? userId)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            Address existedAddress = await _context.GetByAddress(address.Id);

            if (existedAddress != null)
            {
                existedAddress.State = address.State;
                existedAddress.StreetLine1 = address.StreetLine1;
                existedAddress.StreetLine2 = address.StreetLine2;
                existedAddress.City = address.City;

                return existedAddress;
            }

            address.UsersId = userId;
            _context.Add(address);
            await _context.CompleteAsync();

            return address;
        }

        public async Task<IEnumerable<Address>> MyAddress(int? userId)
        {
            return await _context.FindAsync(address => address.UsersId == userId);
        }

        public async Task<string> Subscribe(Carrier carrier, User user)
        {
            try
            {
                var existRelation = await _context.GetSubscribe(user.Id, carrier.Id);
                if (existRelation == null && !carrier.Status)
                {
                    await _context.Subscribe(carrier.Id, user.Id);
                    return "Subscribed";
                }
                await _context.DeleteSubscribe(existRelation);
                return "UnSubscribed";
            }
            catch { return null; }
        }
    }
}