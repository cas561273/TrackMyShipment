using System.Collections.Generic;
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

        public async void AddOrUpdate(Carrier carrier)
        {
            var person = await _context.FindAsync(x => x.Name == carrier.Name);
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

        public async Task<Carrier> GetById(int carrierId)
        {
            return await _context.FindAsync(x => x.Id == carrierId);
        }

        public async Task<bool> Delete(int id)
        {
            var person = await GetById(id);
            if (person != null)
            {
                _context.Remove(person);
                _context.Complete();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Carrier>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<IEnumerable<Carrier>> GetAvailable(User user)
        {
            return await _context.GetAvailable(user);
        }

        public async Task<IEnumerable<Carrier>> GetMyCarriers(User user)
        {
            return await _context.GetCarriers(user);
        }

        public async Task<IEnumerable<User>> GetMyUsers(int carrierId)
        {
            return await _context.GetMyUsers(carrierId);
        }

        public async Task<bool?> ActiveStatus(int carrierId)
        {
            return  await _context.ActiveStatus(carrierId);
        }
    }
}