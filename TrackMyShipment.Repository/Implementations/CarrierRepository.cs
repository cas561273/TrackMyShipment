using System.Collections.Generic;
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

            return await _context.Carriers.WhereAsync(u => u.Cost == 0 && u.Status);
        }

        public async Task<bool?> ChangeStatusCarrierAsync(int carrierId)
        {
            var carrier = await _context.Carriers.SingleOrDefaultAsync(u => u.Id == carrierId);
            if (carrier == null) return null;
            carrier.Status = !carrier.Status;
            await _context.SaveChangesAsync();
            return carrier.Status;
        }
    }
}