using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Helper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Extensions;
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

        public async Task<User> CreateUser(User user)
        {
            var existedUser = await _context.GetUserByEmail(user.Email);
            if (existedUser != null) return null;
            user.Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password);
            user.RoleId = await _context.GetRoleId(Role.CUSTOMER);
            user.SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE);

            var addedUser = await _context.AddAsync(user);

            await _context.CompleteAsync();
            return addedUser.Entity;
        }

        public async Task PutUserCarrier(User carrier)
        {
            var existedUser = await _context.UserExists(carrier);
            if (existedUser == null)
            {
                carrier.RoleId = await _context.GetRoleId(Role.CARRIER);
                carrier.SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE);
                _context.Add(carrier);
            }
            else
            {
                existedUser.FirstName = carrier.FirstName;
                existedUser.LastName = carrier.LastName;
                existedUser.Phone = carrier.Phone;
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
