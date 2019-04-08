using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<bool> PutCompany(string companyName, User existedUser)
        {
            var company = await _context.SingleOrDefaultAsync(c => c.Name.Equals(companyName));
            if (company != null)
            {
                existedUser.CompanyId = company.Id;
                await _context.CompleteAsync();
                return true;
            }

             _context.Add(new Company { Name = companyName });
            await _context.CompleteAsync();
            company = await _context.SingleOrDefaultAsync(c => c.Name.Equals(companyName));
            existedUser.CompanyId = company?.Id;

            await _context.CompleteAsync();
            return true;
        }
    }
}
