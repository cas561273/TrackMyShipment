using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetMyUsers(int carrierId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUser(User person);
        Task PutCarrier(User carrier);
        Task<User> CreateUser(User person);

    }
}