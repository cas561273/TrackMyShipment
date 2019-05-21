
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IObjectiveService
    {
        Task<bool> AddTask(int carrierId, Objective task);
        Task<IEnumerable<Objective>> GetMyTask(int userId);
        Task<bool> ChangeStatusTask(int userId, int taskId);
    }
}
