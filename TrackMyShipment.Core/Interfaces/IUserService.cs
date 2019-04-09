using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetMyUsers(int? carrierId);
        Task<IEnumerable<User>> GetCarrierUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUser(User person);
        Task<User> PutUserCarrier(User carrier);
        Task<User> CreateUser(User person);
        Task<User> EditUserCarrier(User carrier);

    }
}