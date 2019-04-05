using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        User GetByEmail(string email);
        User Get(User person);
        Task<bool> Create(User person, string companyName);
        void PutCarrier(User carrier);
    }
}