using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
           return  await _context.Company.SingleOrDefaultAsync(c => c.Name.Equals(companyName));
        }

    }
}
