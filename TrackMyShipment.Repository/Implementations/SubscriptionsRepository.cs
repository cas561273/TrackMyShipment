using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class SubscriptionsRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly ApplicationContext _context;

        public SubscriptionsRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SubscribeAsync(int? carrierId, int? userId)
        {
            var subscribe = await _context.Supplies.AddAsync(new Supplies { CarrierId = carrierId, UserId = userId });
            if (subscribe.Entity == null) return false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Supplies> GetSubscribeAsync(int? userId, int? carrierId)
        {
            return await _context.Supplies.SingleOrDefaultAsync(u => u.CarrierId == carrierId && u.UserId == userId);
        }

        public async Task<bool> DeleteSubscribeAsync(Supplies relation)
        {
            //var tasks = await _context.Task.WhereAsync(x => x.carrierId == relation.CarrierId);
            //List<int> tasksId =  tasks.SelectAsync(x => x.Id).Result.ToList();

            //var estimates =  _context.Estimates.WhereAsync(x => x.userId == relation.UserId).Result.ToList();
            //var resultEstimates =  await _context.Estimates.WhereAsync(x => estimates.Contains(x));
            //_context.Estimates.RemoveRange(resultEstimates); // delete before unsubcribe

            var subs = _context.Supplies.Remove(relation);
            if (subs == null) return false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
