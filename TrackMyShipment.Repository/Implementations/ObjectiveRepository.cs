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
    public class ViewModelObjective
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Cost { get; set; }
    }
    public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        private readonly ApplicationContext _context;

        public ObjectiveRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ViewModelObjective>> GetMyTask(int userId)
        {
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == userId);
            var subscription = await _context.Supplies.WhereAsync(x => x.UserId == userId);
            var subscriptionId = await subscription.SelectAsync(x => x.CarrierId);

            var tasks = await _context.Task.WhereAsync(x => subscriptionId.Contains(x.carrierId) && x.Status);

            var noSubscriptionTask = await tasks.SelectAsync(x => new ViewModelObjective
            { Id = x.Id, Cost = x.Cost, Name = x.Name, Status = "open" });

            var availableTasks = await _context.Estimates.Include(x => x.Objective)
                .WhereAsync(x => x.userId == userId);
            var viewAvailableTasks = await availableTasks.SelectAsync(x =>
            new ViewModelObjective { Id = x.Objective.Id, Cost = x.Objective.Cost, Name = x.Objective.Name, Status = x.Status });

            var result = noSubscriptionTask.Concat(viewAvailableTasks);
            return result;
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
            var task = await _context.Task.SingleOrDefaultAsync(x => x.Id == taskId);
            task.Status = false;

            if (estimate != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CloseTask(int taskId)
        {
            var task = await _context.Estimates.SingleOrDefaultAsync(x => x.objectiveId == taskId);
            if (task.Status == "resolved")
            {
                task.Status = "completed";
                await CompleteAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ResolveTask(int taskId)
        {
            var task = await _context.Estimates.SingleOrDefaultAsync(x => x.objectiveId == taskId);
            if (task.Status == "in progress")
            {
                task.Status = "resolved";
                await CompleteAsync();
                return true;
            }

            return false;
        }
    }
}
