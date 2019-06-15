using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _context;

        public CompanyService(ICompanyRepository context)
        {
            _context = context;
        }

        public async Task<bool> PutCompanyAsync(string companyName, User existedUser)
        {
            if (companyName == null || companyName == string.Empty)
                companyName = "Private person";

            Company company = await _context.GetCompanyByNameAsync(companyName);
            if (company != null)
            {
                existedUser.CompanyId = company.Id;
            }
            else
            {
                var temp = await _context.AddAsync(new Company {Name = companyName});
                existedUser.CompanyId = temp.Entity.Id;
            }
            await _context.CompleteAsync();
            return true;
        }
    }
}
