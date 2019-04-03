using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Address>
    {
        bool Subscribe(int? carrierId, int? userId);
        Address GetByAddress(int? id);
        Supplies GetSubscribe(int? userId, int? carrierId);
        bool DeleteSubscribe(Supplies relation);

    }
}
