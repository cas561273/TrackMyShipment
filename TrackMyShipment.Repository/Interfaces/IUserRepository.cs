using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        Task<int> GetRoleId(string role);
        Task<int> GetSubscribeId(string subscribe);
        void PutCompany(string companyName, string email);
        User UserExists(User userExist);
    }
}