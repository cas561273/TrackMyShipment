

using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Implementations;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IObjectiveRepository : IRepository<Objective>
    {
        Task<IEnumerable<ViewModelObjective>> GetMyTask(int userId);
        Task<bool> ResolveTask(int taskId);
        Task<int?> GetCarrierId(int carrierUserId);
        Task<bool> ChangeStatusTask(int userId, int taskId);
        Task<bool> TakeTask(int userId, int taskId);
        Task<bool> CloseTask(int taskId);

    }
}
