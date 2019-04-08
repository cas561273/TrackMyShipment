using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Helper;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Role = TrackMyShipment.Repository.Constant.Role;

namespace TrackMyShipment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _context;

        public UserService(IUserRepository context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            var temp = await _context.GetUserByEmail(user.Email);
            if (temp != null) return null;
             _context.Add(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password),
                RoleId = await _context.GetRoleId(Role.CUSTOMER),
                SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE)
            });
            await _context.CompleteAsync();
            return await _context.GetUserByEmail(user.Email);
        }

        public async Task PutCarrier(User carrier)
        {
            var user = await _context.UserExists(carrier);
            if (user == null)
            {
                carrier.RoleId = await _context.GetRoleId(Role.CARRIER);
                carrier.SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE);
                carrier.Password = PasswordHelper.CalculateHashedPassword(carrier.Email, carrier.Password);
                _context.Add(carrier);
            }
            else
            {
                user.FirstName = carrier.FirstName;
                user.LastName = carrier.LastName;
                user.Phone = carrier.Phone;
            }
            await _context.CompleteAsync();
        }

    
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.GetUserByEmail(email);
        }
        public async Task<IEnumerable<User>> GetMyUsers(int carrierId)
        {
            return await _context.GetMyUsers(carrierId);
        }

        public async Task<User> GetUser(User user)
        {
            return await _context.UserExists(user);
        }
    }
}
