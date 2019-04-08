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

        public async Task<User> GetByEmail(string email)
        {
            return await _context.GetByEmail(email);
        }

        public async Task<User> Get(User user)
        {
            return await _context.UserExists(user);
        }

        public async Task<bool> Create(User user, string companyName)
        {
            var temp = await _context.GetByEmail(user.Email);
            if (temp == null)
            {
                await  _context.AddAsync(new User 
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = Encrypt.Sha256(user.Email, user.Password),
                    RoleId = await _context.GetRoleId(Role.Customer),
                    SubscriptionId = await _context.GetSubscribeId(Subscribe.Free)
                });
                 await  _context.CompleteAsync();
                 await  _context.PutCompany(companyName, user.Email);
                return true;
            }
            return false;
        }

        public async void PutCarrier(User carrier)
        {
            var user =  await _context.UserExists(carrier);
            if (user == null)
            {
                carrier.RoleId = await _context.GetRoleId("carrier");
                carrier.SubscriptionId = await _context.GetSubscribeId("free");
                carrier.Password = Encrypt.Sha256(carrier.Email, carrier.Password);

               await _context.AddAsync(carrier);
            }
            else
            {
                user.FirstName = carrier.FirstName;
                user.LastName = carrier.LastName;
                user.Phone = carrier.Phone;
            }
           await _context.CompleteAsync();
        }
    }
}