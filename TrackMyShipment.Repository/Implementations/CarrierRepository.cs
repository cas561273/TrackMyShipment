using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Roles = TrackMyShipment.Repository.Constant.Roles;

namespace TrackMyShipment.Repository.Implementations
{
    public class CarrierRepository : Repository<Carrier>, ICarrierRepository
    {
        private readonly ApplicationContext _context;

        public CarrierRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carrier>> GetCarriersAsync(User user)
        {
            var carriers = await _context.Supplies.Include("Carrier").WhereAsync(u => u.UserId == user.Id);
            return await carriers.SelectAsync(c => c.Carrier);
        }
       

        public async Task<IEnumerable<Carrier>> GetAvailableCarriersAsync(User user)
        {
            if (user.Role.Name.Equals(Roles.ADMIN)) return await _context.Carriers.ToListAsync();

            if (user.Subscription.Status.Equals(Subscribe.PAID))
                return await _context.Carriers.WhereAsync(u => u.Status);

            var supplies = await _context.Supplies.WhereAsync(x => x.UserId == user.Id);
            var suppliesId =  supplies.SelectAsync(x => x.CarrierId).Result.ToList();

            return await _context.Carriers.WhereAsync(u => u.Cost == 0 && u.Status && !suppliesId.Contains(u.Id));
        }

        public async Task<bool?> ChangeStatusCarrierAsync(int carrierId)
        {
            var carrier = await _context.Carriers.SingleOrDefaultAsync(u => u.Id == carrierId);
            if (carrier == null) return null;
            carrier.Status = !carrier.Status;
            await _context.SaveChangesAsync();
            return carrier.Status;
        }
        public async Task<bool> DeleteCarrierAsync(int id)
        {
            var carriersTask = await _context.Task.WhereAsync(x => x.carrierId == id);
            _context.Task.RemoveRange(carriersTask);
            var carriersSupplies = await _context.Supplies.WhereAsync(x => x.CarrierId == id);
            _context.Supplies.RemoveRange(carriersSupplies);
            var carriers = await _context.Carriers.WhereAsync(x => x.Id == id);
            _context.Carriers.RemoveRange(carriers);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}