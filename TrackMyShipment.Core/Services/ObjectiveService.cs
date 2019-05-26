using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Implementations;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    class ObjectiveService:IObjectiveService
    {
        private readonly IObjectiveRepository _context;

        public ObjectiveService(IObjectiveRepository context)
        {
            _context = context;
        }
        public async Task<bool> AddTask(int carrierUserId, Objective task)
        {
            var addedTask = await _context.AddAsync(task);
            int? carrierId = await _context.GetCarrierId(carrierUserId);
            if (addedTask != null && carrierId !=null)
            {
                addedTask.Entity.carrierId = carrierId;
                addedTask.Entity.Status = true;
                await _context.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ViewModelObjective>> GetMyTask(int userId)
        {
            return  await _context.GetMyTask(userId);
        }

        public async Task<bool> ChangeStatusTask(int userId, int taskId)
        {
            return await _context.ChangeStatusTask(userId, taskId);
        }
        public async Task<bool> TakeTask(int userId, int taskId)
        {
            return await _context.TakeTask(userId, taskId);
        }
        public async Task<bool> ResolveTask(int taskId)
        { 
            return await _context.ResolveTask(taskId);
        }
        public async Task<bool> CloseTask(int taskId)
        {
            return await _context.CloseTask(taskId);
        }
    }
}
