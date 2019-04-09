using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Helper;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        private async Task<User> FetchUser(Expression<Func<User, bool>> predicate)
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

        public async Task<IEnumerable<User>> GetMyUsers(int carrierId)
        {
            var relation = await _context.Supplies.Include("User").WhereAsync(u => u.CarrierId == carrierId);
            return await Task.Run(() => relation.Select(u => u.User));
        }

        public async Task<User> UserExists(User userExist)
        {
            var encryptedPassword = PasswordHelper.CalculateHashedPassword(userExist.Email, userExist.Password);
            return await FetchUser(u => u.Email.Equals(userExist.Email) && u.Password.Equals(encryptedPassword));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await FetchUser(_ => _.Email.Equals(email));
        }

        public async Task<int> GetSubscribeId(string subscribeStatus)
        {
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(s => s.Status.Equals(subscribeStatus));
            return subscription.Id;
        }
        public async Task<int> GetRoleId(string roleName)
        {
            var myRole = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(roleName));
            return myRole.Id;
        }
    }
}