using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<int> GetRoleId(string role);
        Task<int> GetSubscribeId(string subscribe);
        Task<bool> PutCompany(string companyName, string email);
        Task<User> UserExists(User userExist);
    }
}