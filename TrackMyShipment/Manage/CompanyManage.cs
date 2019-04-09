using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Manage
{
    public class CompanyManage
    {
        private readonly ICompanyService _companyService;

        public CompanyManage(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<bool> AddCompanyToUser(User user,string companyName)
        {
            return await _companyService.PutCompanyAsync(companyName, user);
        }
    }
}

