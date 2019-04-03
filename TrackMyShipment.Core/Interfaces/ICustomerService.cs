using System.Collections.Generic;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICustomerService
    {
        Address PutOrUpdate(Address address, int? userId);
        string Subscribe(int? carrierId, int? userId);
        Address DeleteAddress(int? id, int? userId);
        string StatusAddress(int? id, int? userId);
        string DeleteSubscribe(Supplies relation);
        Supplies GetSubscribe(int? userId, int? carrierId);
        IEnumerable<Address> MyAddress(int? userId);
    }
}