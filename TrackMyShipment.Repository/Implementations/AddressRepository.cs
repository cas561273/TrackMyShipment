using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implementations
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationContext _context;

        public AddressRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Address> GetByAddress(int? id)
        {
            return await _context.Address.SingleOrDefaultAsync(_ => _.Id.Equals(id));
        }
     
    }
}