using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
