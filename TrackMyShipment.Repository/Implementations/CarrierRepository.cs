using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Role = TrackMyShipment.Repository.Constant.Role;

namespace TrackMyShipment.Repository.Implementations
{
    public class CarrierRepository : Repository<Carrier>, ICarrierRepository
    {
        private readonly ApplicationContext _context;

        public CarrierRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carrier>> GetCarriers(User user)
        {
            var carriers = await _context.Supplies.Include("Carrier").WhereAsync(u => u.UserId == user.Id);
            return await Task.Run(() => carriers.Select(c => c.Carrier));
        }

        public async Task<IEnumerable<Carrier>> GetAvailable(User user)
        {
            if (user.Role.Name.Equals(Role.ADMIN)) return await _context.Carriers.ToListAsync();

            if (user.Subscription.Status.Equals(Subscribe.PAID))
                return await _context.Carriers.WhereAsync(u => u.Status);

            return await _context.Carriers.WhereAsync(u => u.Cost == 0 && u.Status);
        }

        public async Task<IEnumerable<User>> GetMyUsers(int carrierId)
        {
            var relation = await _context.Supplies.Include("User").WhereAsync(u => u.CarrierId == carrierId);
            return await Task.Run(() => relation.Select(u => u.User));
        }

        public async Task<bool?> ActiveStatus(int carrierId)
        {
            var carrier = await _context.Carriers.SingleOrDefaultAsync(u => u.Id == carrierId);

            if (carrier == null) return null;

            carrier.Status = !carrier.Status;
            await _context.SaveChangesAsync();
            return carrier.Status;
        }
    }
}