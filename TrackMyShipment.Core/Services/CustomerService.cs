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
            _context.CompleteAsync();
            return address;
        }

        public async Task<string> StatusAddress(int? id, int? userId)
        {
            var address =  await _context.GetByAddress(id);
            if (address.UsersId != userId) return null;
            address.Active = !address.Active;
             _context.CompleteAsync();
            return "Address successfully deleted";
        }


        public async Task<string> DeleteSubscribe(Supplies relation)
        {
            var result = await _context.DeleteSubscribe(relation);
            return result ? "Delete Successfully" : null;
        }

        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return  await _context.GetSubscribe(userId, carrierId);
        }

        public async Task<Address> PutOrUpdate(Address address, int? userId)
        {
            try
            {
                var existAddress =  await _context.GetByAddress(address.Id);
                if (existAddress != null)
                {
                    existAddress.State = address.State;
                    existAddress.StreetLine1 = address.StreetLine1;
                    existAddress.StreetLine2 = address.StreetLine2;
                    existAddress.City = address.City;
                }
                else
                {
                    address.UsersId = userId;
                    _context.Add(address);
                }
                 _context.CompleteAsync();
                return address;
            }
            catch {return null;}
        }

        public async Task<IEnumerable<Address>> MyAddress(int? userId)
        {
            return await Task.Run(() => _context.Find(address => address.UsersId == userId));
        }

        public async Task<string> Subscribe(int? carrierId, int? userId)
        {
            try
            {
                var existRelation =  await _context.GetSubscribe(userId, carrierId);

                if (existRelation == null)
                {
                   await _context.Subscribe(carrierId, userId);
                    return "Subscribed";
                }

                await _context.DeleteSubscribe(existRelation);

                return "UnSubscribed";
            }
            catch
            {
                return null;
            }
        }
    }
}