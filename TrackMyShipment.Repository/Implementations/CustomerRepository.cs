using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class CustomerRepository : Repository<Address>, ICustomerRepository
    {
        private readonly ApplicationContext _context;

        public CustomerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Subscribe(int? carrierId, int? userId)
        {
            var subscribe = await _context.Supplies.AddAsync(new Supplies {CarrierId = carrierId, UserId = userId});
            if (subscribe == null) return false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetByAddress(int? id)
        {
            return await _context.Address.SingleOrDefaultAsync(_ => _.Id.Equals(id));
        }

        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return await _context.Supplies.SingleOrDefaultAsync(u => u.CarrierId == carrierId && u.UserId == userId);
        }

        public async Task<bool> DeleteSubscribe(Supplies relation)
        {
            var subs = _context.Supplies.Remove(relation);
            if (subs == null) return false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}