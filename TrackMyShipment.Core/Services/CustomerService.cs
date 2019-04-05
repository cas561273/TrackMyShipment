using System.Collections.Generic;
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

        public  Address DeleteAddress(int? id, int? userId)
        {
            var address =  _context.GetByAddress(id);
            if (address.UsersId != userId) return null;
            _context.Remove(address);
            _context.Complete();
            return address;
        }

        public  string StatusAddress(int? id, int? userId)
        {
            var address =  _context.GetByAddress(id);
            if (address.UsersId != userId) return null;
            address.Active = !address.Active;
            _context.Complete();
            return "Address successfully deleted";
        }


        public string DeleteSubscribe(Supplies relation)
        {
            var result = _context.DeleteSubscribe(relation);
            return result ? "Delete Successfully" : null;
        }

        public Supplies GetSubscribe(int? userId, int? carrierId)
        {
            return  _context.GetSubscribe(userId, carrierId);
        }

        public Address PutOrUpdate(Address address, int? userId)
        {
            try
            {
                var existAddress =  _context.GetByAddress(address.Id);
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
                _context.Complete();
                return address;
            }
            catch {return null;}
        }

        public IEnumerable<Address> MyAddress(int? userId)
        {
            return _context.Find(address => address.UsersId == userId);
        }

        public string Subscribe(int? carrierId, int? userId)
        {
            try
            {
                var existRelation =  _context.GetSubscribe(userId, carrierId);

                if (existRelation == null)
                {
                    _context.Subscribe(carrierId, userId);
                    return "Subscribed";
                }

                _context.DeleteSubscribe(existRelation);

                return "UnSubscribed";
            }
            catch
            {
                return null;
            }
        }
    }
}