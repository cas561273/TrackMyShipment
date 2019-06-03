using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _context.Entry(user).Reference(nameof(Subscription)).Load();
                _context.Entry(user).Reference(nameof(Company)).Load();
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

        public async Task<bool> DeleteUserCarrier(int idUserCarrier)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == idUserCarrier);
            var userDeleted = _context.Users.Remove(user);
            if (userDeleted != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetCarrierUsersAsync()
        {
            IEnumerable<User> carrier = await _context.Users.Include(nameof(Role)).WhereAsync(u => u.Role.Name.Equals(Roles.CARRIER));
            return carrier;
        }

        public async Task<IEnumerable<User>> GetCarrierUsersByIdAsync(int id)
        {
            var supplies = await _context.Supplies.Include(x => x.User.Role).
                WhereAsync(u => u.CarrierId == id && u.User.Role.Name.Equals(Roles.CARRIER));
            return await supplies.SelectAsync(u => u.User);
        }

        public async Task<List<int>> GetStats()
        {
            List<int> stats = new List<int>();
            var countCarrier =  _context.Users.WhereAsync(x => x.RoleId == 3).Result.Count();
            var completedTask = _context.Estimates.Include(x => x.Objective)
                .WhereAsync(x => x.Status.Equals("completed")).Result.Count();
            var sum =  _context.Estimates.Include(x => x.Objective)
                .WhereAsync(x=>x.Status.Equals("completed")).Result
           .Sum(x => Convert.ToInt32(x.Objective.Cost));
            stats.Add(completedTask);
            stats.Add(countCarrier);
            stats.Add(sum);
            return stats;
        }

        public async Task<List<int>> MyProfileStats(int id)
        {
            List<int> stats = new List<int>();
            var earnedMoney = _context.Estimates.Include(x => x.Objective)
                .WhereAsync(x => x.Status.Equals("completed") && x.userId == id).Result.Sum(y=>Convert.ToInt32(y.Objective.Cost));

            var activeTask = _context.Estimates.Include(x => x.Objective)
                .WhereAsync(x => x.Status.Equals("in progress") && x.userId == id).Result.Count();

            var completedTask = _context.Estimates.Include(x => x.Objective)
               .WhereAsync(x => x.Status.Equals("completed") && x.userId == id).Result.Count();

            stats.Add(earnedMoney);
            stats.Add(activeTask);
            stats.Add(completedTask);

            return stats;
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

        public async Task<IEnumerable<Object>> GetWorkUsers(int id)
        {
            var supplies = await _context.Supplies.SingleOrDefaultAsync(x => x.UserId == id);
            var tasks = await _context.Task.WhereAsync(x => x.carrierId == supplies.CarrierId);
            var tasksId =  tasks.SelectAsync(x => x.Id).Result.ToList();
            var estimates =  _context.Estimates.Include(x => x.Objective)
                .Include(x => x.User).WhereAsync(x => tasksId.Contains(x.objectiveId)).Result
                .Select(x => new {user = x.User, task = x.Objective, status = x.Status});


            return estimates;
        }

    }
}