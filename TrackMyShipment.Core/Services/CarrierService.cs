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

        public async Task<bool> AddOrUpdateCarrier(Carrier carrier)
        {
            var person = await _context.SingleOrDefaultAsync(c => c.Name == carrier.Name);
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

            await _context.CompleteAsync();
            return true;
        }

        public async Task<Carrier> GetByIdCarrier(int carrierId)
        {
            return await _context.SingleOrDefaultAsync(x => x.Id == carrierId);
        }

        public async Task<bool> DeleteCarrier(int id)
        {
            var carrier = await GetByIdCarrier(id);
            if (carrier != null)
            {
                _context.Remove(carrier);
                await _context.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Carrier>> GetAvailableCarriers(User user)
        {
            return await _context.GetAvailable(user);
        }

        public async Task<IEnumerable<Carrier>> GetMyCarriers(User user)
        {
            return await _context.GetCarriers(user);
        }


        public async Task<bool?> ChangeStatusCarrier(int carrierId)
        {
            return await _context.ChangeStatusCarrier(carrierId);
        }

        public async Task<IEnumerable<Carrier>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }
    }
}