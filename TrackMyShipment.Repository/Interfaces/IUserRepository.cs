using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<int> GetRoleId(string roleName);
        Task<User> UserExists(User userExist);
        Task<IEnumerable<User>> GetMyUsers(int carrierId);
        Task<int> GetSubscribeId(string subscribe);

    }
}