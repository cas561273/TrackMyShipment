using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetMyUsersAsync(int? carrierId);
        Task<IEnumerable<User>> GetCarrierUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserAsync(User person);
        Task<User> PutUserCarrierAsync(User carrier);
        Task<User> CreateUserAsync(User person);
        Task<User> EditUserCarrierAsync(User carrier);

    }
}