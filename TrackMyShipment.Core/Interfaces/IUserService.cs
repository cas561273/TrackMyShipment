using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByEmail(string email);
        Task<User> Get(User person);
        Task<bool> Create(User person, string companyName);
        Task PutCarrier(User carrier);
    }
}