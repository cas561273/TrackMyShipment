using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> UserExists(User userExist)
        {
            var encryptedPassword = Encrypt.Sha256(userExist.Email, userExist.Password);
            return await FetchUser(u => u.Email.Equals(userExist.Email) && u.Password.Equals(encryptedPassword));
        }

        public async Task<User> GetByEmail(string email)
        {
            return await FetchUser(_ => _.Email.Equals(email));
        }

        public async Task<int> GetRoleId(string role)
        {
            var myRole = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(role));
            return myRole.Id;
        }

        public async Task<int> GetSubscribeId(string subscribe)
        {
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(s => s.Status.Equals(subscribe));
            return subscription.Id;
        }

        public async Task<bool> PutCompany(string companyName, string email)
        {
            var currentUser = await GetByEmail(email);
            var company = await _context.Company.SingleOrDefaultAsync(c => c.Name.Equals(companyName));
            if (company != null)
            {
                currentUser.CompanyId = company.Id;
                await _context.SaveChangesAsync();
                return true;
            }

            await _context.Company.AddAsync(new Company {Name = companyName});
            await _context.SaveChangesAsync();
            company = await _context.Company.SingleOrDefaultAsync(c => c.Name.Equals(companyName));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(email));
            if (user != null) user.CompanyId = company?.Id;

            await _context.SaveChangesAsync();
            return true;
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
    }
}