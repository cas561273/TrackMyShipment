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

        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return await _context.GetSubscribe(userId, carrierId);
        }

        public async Task<bool> Subscribe(Carrier carrier, User user)
        {
            var existRelation = await _context.GetSubscribe(user.Id, carrier.Id);
            if (existRelation == null && !carrier.Status)
            {
                await _context.Subscribe(carrier.Id, user.Id);
                return true;
            }
            await _context.DeleteSubscribe(existRelation);
            return true;
        }
    }
}