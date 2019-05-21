using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
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
                await _context.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Objective>> GetMyTask(int userId)
        {
            return  await _context.GetMyTask(userId);
        }
    }
}
