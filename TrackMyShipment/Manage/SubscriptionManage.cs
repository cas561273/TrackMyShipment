using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Manage
{
    public class SubscriptionManage
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionManage(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        public async Task<Supplies> GetSubscribe(int? userId, int? carrierId)
        {
            return await _subscriptionService.GetSubscribeAsync(userId, carrierId);
        }

        public async Task<bool> Subscribe(Carrier carrier, User user)
        {
            return await _subscriptionService.SubscribeAsync(carrier, user);
        }
    }
}
