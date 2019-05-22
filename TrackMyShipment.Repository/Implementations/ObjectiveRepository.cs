using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Extensions;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        private readonly ApplicationContext _context;

        public ObjectiveRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Objective>> GetMyTask(int userId)
        {
            var subscriptionCarrier = await _context.Supplies.WhereAsync(x => x.UserId == userId);
            List<int?> carriersId = subscriptionCarrier.SelectAsync(x => x.CarrierId).Result.ToList();
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == userId);
            if (user.Role.Name == "carrier")
                return await _context.Task.WhereAsync(x => carriersId.Contains(x.carrierId));
            return await _context.Task.WhereAsync(x => carriersId.Contains(x.carrierId) && x.Status == true);
        }

        public async Task<int?> GetCarrierId(int carrierUserId)
        {
            var carrier = await _context.Supplies.SingleOrDefaultAsync(x => x.UserId == carrierUserId);
            return carrier.CarrierId;
        }

        public async Task<bool> ChangeStatusTask(int userId, int taskId)
        {
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == userId);
            var task = await _context.Task.SingleOrDefaultAsync(x => x.Id == taskId);
            if (user != null && task != null)
            {
                task.Status = !task.Status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> TakeTask(int userId, int taskId)
        {
            var estimate = await _context.Estimates.AddAsync(new Estimate()
            { userId = userId, objectiveId = taskId, Status = "in progress" });
            if (estimate != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CloseTask(int taskId)
        {
            var task= await _context.Estimates.SingleOrDefaultAsync(x => x.objectiveId == taskId);
            if (task.Status == "resolved")
            {
                task.Status = "done";
                await CompleteAsync();
                return true;
            }

            return false;
        }
    }
}
