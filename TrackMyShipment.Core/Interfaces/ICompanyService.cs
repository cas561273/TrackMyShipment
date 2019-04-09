using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<bool> PutCompanyAsync(string companyName, User user);
    }
}
