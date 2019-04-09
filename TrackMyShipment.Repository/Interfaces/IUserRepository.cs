using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId);
        Task<IEnumerable<User>> GetCarrierUsersAsync();
        Task<User> GetUserByIdAsync(int idUser);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> UserExistsAsync(User userExist);
        Task<int> GetSubscribeIdAsync(string subscribeStatus);
        Task<int> GetRoleIdAsync(string roleName);

    }
}