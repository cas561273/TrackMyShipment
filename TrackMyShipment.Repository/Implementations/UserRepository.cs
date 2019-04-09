using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.Repository.Constant;

namespace TrackMyShipment.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        private async Task<User> FetchUserAsync(Expression<Func<User, bool>> predicate)
        {
            var user = await _context.Users.SingleOrDefaultAsync(predicate);
            if (user != null)
            {
                _context.Entry(user).Reference(nameof(Role)).Load();
                _context.Entry(user).Reference(nameof(Company)).Load();
                _context.Entry(user).Reference(nameof(Subscription)).Load();
            }
            return user;
        }

        public async Task<User> GetUserByIdAsync(int idUser)
        {
           return await _context.Users.SingleOrDefaultAsync(u => u.Id == idUser);
        }

        public async Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId)
        {
            var relation = await _context.Supplies.Include("User").WhereAsync(u => u.CarrierId == carrierId);
            return await Task.Run(() => relation.Select(u => u.User));
        }
        public async Task<IEnumerable<User>> GetCarrierUsersAsync()
        {
            var carrier = await _context.Users.Include("Role").WhereAsync(u => u.Role.Name==Roles.CARRIER);
            return carrier;
        }

        public async Task<User> UserExistsAsync(User userExist)
        {
           return await FetchUserAsync(u => u.Email==userExist.Email && u.Password==userExist.Password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await FetchUserAsync(_ => _.Email.Equals(email));
        }

        public async Task<int> GetSubscribeIdAsync(string subscribeStatus)
        {
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(s => s.Status.Equals(subscribeStatus));
            return subscription.Id;
        }
        public async Task<int> GetRoleIdAsync(string roleName)
        {
            var myRole = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(roleName));
            return myRole.Id;
        }
    }
}