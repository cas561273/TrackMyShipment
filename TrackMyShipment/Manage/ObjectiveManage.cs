using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Manage
{
    public class ObjectiveManage
    {
        private readonly IObjectiveService _objectiveService;

        public ObjectiveManage(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        public async Task<bool> AddTask(int carrierId,Objective task)
        { 
            return await _objectiveService.AddTask(carrierId, task);
        }
        public async Task<IEnumerable<Objective>> GetMyTask(int userId)
        {
            return await _objectiveService.GetMyTask(userId);
        }

    }

}