using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetCarrierUsersByIdAsync(int id);
        Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId);
        Task<IEnumerable<User>> GetCarrierUsersAsync();
        Task<IEnumerable<object>> GetWorkUsers(int id);

        Task<User> GetUserByIdAsync(int idUser);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> UserExistsAsync(User userExist);
        Task<bool> DeleteUserCarrier(int idUserCarrier);

        Task<int> GetSubscribeIdAsync(string subscribeStatus);
        Task<int> GetRoleIdAsync(string roleName);
        Task<List<int>> MyProfileStats(int id);

        Task<List<int>> GetStats();
    }
}