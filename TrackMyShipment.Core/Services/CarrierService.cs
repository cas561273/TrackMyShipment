using System.Collections.Generic;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class CarrierService : ICarrierService
    {
        public readonly ICarrierRepository _context;
        public CarrierService(ICarrierRepository context)
        {
            _context = context;
        }

        public void AddOrUpdate(Carrier carrier)
        {
            Carrier person = _context.GetByName(carrier.Name);
            if (person == null)
            {
                _context.Add(carrier);
            }
            else
            {
                person.Name = carrier.Name;
                person.Logo = carrier.Logo;
                person.Phone = carrier.Phone;
                person.Status = carrier.Status;
                person.Code = carrier.Code;
            }
            _context.Complete();
        }

        public Carrier Get(Carrier carrier)
        {
            Carrier person = _context.Get(carrier.Id);
            return person;
        }

        public Carrier GetById(int carrierId)
        {
            return _context.Get(carrierId);
        }
        public bool Delete(int id)
        {
            Carrier person = _context.Get(id);
            if (person != null)
            {
                _context.Remove(person);
                _context.Complete();
                return true;
            }
            return false;
        }
        public IEnumerable<Carrier> GetAll()
        {
            return _context.GetAll();
        }

        public IEnumerable<Carrier> GetAvailable(User user)
        {

            IEnumerable<Carrier> carriers = _context.GetAvailable(user);

            return carriers;
        }

        public IEnumerable<Carrier> GetMyCarriers(User user)
        {
            return _context.GetCarriers(user);
        }

        public IEnumerable<User> GetMyUsers(int carrierId)
        {
            return _context.GetMyUser(carrierId);
        }

        public bool Active(int carrierId)
        {
            var status = _context.Active(carrierId);
            _context.Complete();
            return status;
        }



    }
}