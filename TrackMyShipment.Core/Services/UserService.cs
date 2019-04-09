using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Helper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Roles = TrackMyShipment.Repository.Constant.Roles;

namespace TrackMyShipment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _context;

        public UserService(IUserRepository context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User existedUser = await _context.GetUserByEmailAsync(user.Email);
            if (existedUser != null) return null;
            user.RoleId = await _context.GetRoleIdAsync(Roles.CUSTOMER);
            user.SubscriptionId = await _context.GetSubscribeIdAsync(Subscribe.FREE);

            var addedUser = await _context.AddAsync(user);

            await _context.CompleteAsync();
            return addedUser.Entity;
        }

        public async Task<User> PutUserCarrierAsync(User carrier)
        {
            User existedUser = await _context.UserExistsAsync(carrier);
            if (existedUser == null)
            {
                carrier.RoleId = await _context.GetRoleIdAsync(Roles.CARRIER);
                carrier.SubscriptionId = await _context.GetSubscribeIdAsync(Subscribe.FREE);
                var addedUser  =  await _context.AddAsync(carrier);
                return addedUser.Entity;
            }
            return null;
        }

        public async Task<User> EditUserCarrierAsync(User carrier)
        {
            User existedUser = await _context.GetUserByIdAsync(carrier.Id);
            if (existedUser != null)
            {
                existedUser.FirstName = carrier.FirstName;
                existedUser.LastName = carrier.LastName;
                existedUser.Phone = carrier.Phone;
            }
            await _context.CompleteAsync();
            return existedUser;
        }

        public async Task<IEnumerable<User>> GetCarrierUsersAsync()
        {
           return await _context.GetCarrierUsersAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.GetUserByEmailAsync(email);
        }
        public async Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId)
        {
            return await _context.GetMyUsersAsync(carrierId);
        }

        public async Task<User> GetUserAsync(User user)
        {
            return await _context.UserExistsAsync(user);
        }
    }
}
