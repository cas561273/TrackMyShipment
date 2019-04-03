using System.Collections.Generic;
using System.Linq;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implemetations
{
    public class CarrierRepository : Repository<Carrier>, ICarrierRepository
    {
        public ApplicationContext Context;
        public CarrierRepository(ApplicationContext context) : base(context)
        {
            Context = context;
        }

        public Carrier GetByName(string name)
        {
            return SingleOrDefault(_ => _.Name.Equals(name));
        }


        public IEnumerable<Carrier> GetCarriers(User user)
        {
            return Context.Supplies.Where(u => u.UserId == user.Id).Select(u => u.Carrier);
        }

        public IEnumerable<Carrier> GetAvailable(User user)
        {
            if (user.Role.Role.Equals("admin"))
            {
                return Context.Carriers.ToList();
            }

            if (user.Subscription.Equals("paid"))
            {
                return  Context.Carriers.Where(u => u.Status == true);
            }

            else return GetFree();
            
        }

        public IEnumerable<Carrier> GetFree()
        {
            return Context.Carriers.Where(u => u.Cost == 0 && u.Status == true);
        }

        public IEnumerable<User> GetMyUser(int carrierId)
        {
            IQueryable<User> users = Context.Supplies.Where(u => u.CarrierId == carrierId).Select(u => u.User);
            return users;
        }

        public bool Active(int carrierId)
        {
            Carrier carrier = Context.Carriers.Where(u => u.Id == carrierId).SingleOrDefault();
            carrier.Status = !carrier.Status;
            if (carrier.Status)
                return true;
            else return false;
        }
    }
}
