using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Const;
using TrackMyShipment.Repository.Helper;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Role = TrackMyShipment.Repository.Const.Role;

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
            return  _context.GetByEmail(email);
        }

        public  User Get(User user)
        {
            return  _context.UserExists(user);
        }

        public async Task<bool> Create(User user, string companyName)
        {
            var temp =  _context.GetByEmail(user.Email);
            if (temp == null)
            {
                _context.Add(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = Encrypt.Sha256(user.Email, user.Password),
                    RoleId = await _context.GetRoleId(Role.Customer),
                    SubscriptionId = await _context.GetSubscribeId(Subscribe.Free)
                });
                _context.Complete();
                _context.PutCompany(companyName, user.Email);
                return true;
            }

            return false;
        }

        public async void PutCarrier(User carrier)
        {
            var user =  _context.UserExists(carrier);
            if (user == null)
            {
                carrier.RoleId = _context.GetRoleId("carrier").Result;
                carrier.SubscriptionId = await _context.GetSubscribeId("free");
                carrier.Password = Encrypt.Sha256(carrier.Email, carrier.Password);

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