using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetMyUsers(int carrierId);
        Task<User> GetUserByEmail(string email);
        Task<User> UserExists(User userExist);
        Task<int> GetSubscribeId(string subscribeStatus);
        Task<int> GetRoleId(string roleName);

    }
}