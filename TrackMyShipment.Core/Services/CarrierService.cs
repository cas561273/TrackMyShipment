using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _context;

        public CarrierService(ICarrierRepository context)
        {
            _context = context;
        }

        public void AddOrUpdate(Carrier carrier)
        {
            var person = _context.Find(x => x.Name == carrier.Name).FirstOrDefault();
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

        public Carrier GetById(int carrierId)
        {
            return _context.Find(x => x.Id == carrierId).FirstOrDefault();
        }

        public bool Delete(int id)
        {
            var person = GetById(id);
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

        public async Task<IEnumerable<Carrier>> GetAvailable(User user)
        {
            return await _context.GetAvailable(user);
        }

        public IEnumerable<Carrier> GetMyCarriers(User user)
        {
            return _context.GetCarriers(user);
        }

        public IEnumerable<User> GetMyUsers(int carrierId)
        {
            return _context.GetMyUser(carrierId);
        }

        public bool? ActiveStatus(int carrierId)
        {
            return _context.ActiveStatus(carrierId);
        }
    }
}