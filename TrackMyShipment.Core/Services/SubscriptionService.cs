using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
   public class SubscriptionService:ISubscriptionService
    {
        private readonly ISubscriptionRepository _context;

        public SubscriptionService(ISubscriptionRepository context)
        {
            _context = context;
        }

        public async Task<Supplies> GetSubscribeAsync(int? userId, int? carrierId)
        {
            return await _context.GetSubscribeAsync(userId, carrierId);
        }

        public async Task<bool> SubscribeAsync(Carrier carrier, User user)
        {
            var existRelation = await _context.GetSubscribeAsync(user.Id, carrier.Id);
            if (existRelation == null && !carrier.Status)
            {
                await _context.SubscribeAsync(carrier.Id, user.Id);
                return true;
            }
            await _context.DeleteSubscribeAsync(existRelation);
            return true;
        }
    }
}