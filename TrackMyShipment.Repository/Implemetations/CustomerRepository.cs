using System.Collections.Generic;
using System.Linq;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implemetations
{
    public class CustomerRepository : Repository<Address>, ICustomerRepository
    {
        public ApplicationContext Context;
        public CustomerRepository(ApplicationContext context) : base(context)
        {
            Context = context;
        }

        public bool Subscribe(int? carrierId, int? userId)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Supplies> subscribe = Context.Supplies.Add(new Supplies { CarrierId = carrierId, UserId = userId });
            if (subscribe != null)
            {
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public Address GetByAddress(int? id)
        {

            Address tempAddress = Context.Address.SingleOrDefault(_ => _.Id.Equals(id));
            if (tempAddress != null)
            {
                return tempAddress;
            }
            else
            {
                return null;
            }
        }

        public Supplies GetSubscribe(int? userId, int? carrierId)
        {
            return Context.Supplies.SingleOrDefault(u => u.CarrierId == carrierId && u.UserId == userId);
        }
        public bool DeleteSubscribe(Supplies relation)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Supplies> subs = Context.Supplies.Remove(relation);
            if (subs != null)
            {
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<User> GetCarrierUser(int? userId, int? carrierId)
        {
            return Context.Supplies.Where(u => u.CarrierId == carrierId && u.UserId == userId).Select(u => u.User);

        }
    }
}
