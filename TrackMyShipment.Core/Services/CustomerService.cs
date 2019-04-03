using System.Collections.Generic;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _context;

        public CustomerService(ICustomerRepository context)
        {
            _context = context;
        }

        public Address DeleteAddress(int? id, int? userId)
        {

            Address address = _context.GetByAddress(id);
            if (address.UsersId == userId)
            {
                _context.Remove(address);
                _context.Complete();
                return address;
            }
            return null;

        }

        public string StatusAddress(int? id, int? userId)
        {

            Address address = _context.GetByAddress(id);
            if (address.UsersId != userId)
            {
                return null;
            }
            address.Active = !address.Active;
            _context.Complete();
            return "Address successfully deleted";

        }


        public string DeleteSubscribe(Supplies relation)
        {

            bool result = _context.DeleteSubscribe(relation);
            if (result != false)
            {
                return "Delete Successfuly";
            }

            return null;
        }

        public Supplies GetSubscribe(int? userId, int? carrierId)
        {
            return _context.GetSubscribe(userId, carrierId);
        }

        public Address PutOrUpdate(Address address, int? userId)
        {
            try
            {
                Address existAddress = _context.GetByAddress(address.Id);

                if (existAddress == null)
                {
                    address.UsersId = userId;
                    _context.Add(address);
                }

                else
                {
                    existAddress.State = address.State;
                    existAddress.StreetLine1 = address.StreetLine1;
                    existAddress.StreetLine2 = address.StreetLine2;
                    existAddress.City = address.City;
                }
                _context.Complete();
                return address;

            }

            catch { return null; }
        }

        public IEnumerable<Address> MyAddress(int? userId)
        {
            return _context.Find(address => address.UsersId == userId);
        }

        public string Subscribe(int? carrierId, int? userId)
        {
            try
            {
                Supplies existRelation = _context.GetSubscribe(userId, carrierId);

                if (existRelation == null)
                {
                    _context.Subscribe(carrierId, userId);
                    return "Subscribed";

                }
                _context.DeleteSubscribe(existRelation);

                return "UnSubscribed";
            }
            catch { return null; }
        }
    }
}
