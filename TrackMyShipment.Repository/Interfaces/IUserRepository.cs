using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        User UserExists(User user);
        int GetRoleId(string role);
        int GetSubscribeId(string subscribe);
        void PutCompany(string companyName, string email);

    }
}
