using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Core.Utils;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _context;

        public UserService(IUserRepository context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _context.GetByEmail(email);
        }

        public User Get(User user)
        {
            return _context.UserExists(user);
        }


        public bool Create(User user,string companyName)
        {
            User temp = _context.GetByEmail(user.Email);
            if (temp == null)
            {
                _context.Add((new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = Encrypt.Sha256(user.Email,user.Password),
                    RoleId = _context.GetRoleId("customer"),
                    SubscriptionId = _context.GetSubscribeId("free")
                }));
                _context.Complete();
                _context.PutCompany(companyName,user.Email);
                _context.Complete();
                return true;
            }
            return false;
        }


        public void PutCarrier(User carrier)
        {
            User user = _context.UserExists(carrier);
            if (user == null)
            {
                carrier.RoleId = _context.GetRoleId("carrier");
                carrier.SubscriptionId = _context.GetSubscribeId("free");
                _context.Add(carrier);
            }
            else
            {
                user.FirstName = carrier.FirstName;
                user.LastName = carrier.LastName;
                user.Phone = carrier.Phone;
            }
            _context.Complete();
        }
    }
}