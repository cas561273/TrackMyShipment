using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Implementations;
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
        public async Task<IEnumerable<ViewModelObjective>> GetMyTask(int userId)
        {
            return await _objectiveService.GetMyTask(userId);
        }

        public async Task<bool> ChangeStatusTask(int userId,int taskId)
        {
            
            return await _objectiveService.ChangeStatusTask(userId,taskId);
        }

        public async Task<bool> TakeTask(int userId, int taskId)
        {
            return await _objectiveService.TakeTask(userId, taskId);
        }
        public async Task<bool> ResolveTask(int taskId)
        {
            return await _objectiveService.ResolveTask(taskId);
        }
        public async Task<bool> CloseTask(int taskId)
        {
            return await _objectiveService.CloseTask(taskId);
        }
    }

}