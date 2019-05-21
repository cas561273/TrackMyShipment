using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Extensions;
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
        private async Task<User> FetchUserAsync(Expression<Func<User, bool>> predicate)
        {
            User user = await _context.Users.SingleOrDefaultAsync(predicate);
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

        //Customer
        public async Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId)
        {
            var supplies = await _context.Supplies.Include(x => x.User.Role).
                WhereAsync(u => u.CarrierId == carrierId && u.User.Role.Name.Equals(Roles.CUSTOMER));
            return await supplies.SelectAsync(u => u.User);
        }
        public async Task<IEnumerable<User>> GetCarrierUsersAsync()
        {
            IEnumerable<User> carrier = await _context.Users.Include(nameof(Role)).WhereAsync(u => u.Role.Name.Equals(Roles.CARRIER));
            return carrier;
        }

        //Carriers
        public async Task<IEnumerable<User>> GetCarrierUsersByIdAsync(int id)
        {
            var supplies = await _context.Supplies.Include(x=> x.User.Role).
                WhereAsync(u => u.CarrierId == id && u.User.Role.Name.Equals(Roles.CARRIER));
            return await supplies.SelectAsync(u => u.User);
        }
    
        public async Task<User> UserExistsAsync(User userExist)
        {
            return await FetchUserAsync(u => u.Email == userExist.Email && u.Password == userExist.Password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await FetchUserAsync(_ => _.Email.Equals(email));
        }

        public async Task<int> GetSubscribeIdAsync(string subscribeStatus)
        {
            Subscription subscription = await _context.Subscriptions.SingleOrDefaultAsync(s => s.Status.Equals(subscribeStatus));
            return subscription.Id;
        }
        public async Task<int> GetRoleIdAsync(string roleName)
        {
            Role myRole = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(roleName));
            return myRole.Id;
        }
    }
}