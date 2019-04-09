﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.Repository.Extensions;

namespace TrackMyShipment.Core.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _context;

        public CarrierService(ICarrierRepository context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateCarrierAsync(Carrier carrier)
        {
            Carrier existedCarrier = await _context.SingleOrDefaultAsync(c => c.Name == carrier.Name);
       
            if (existedCarrier == null)
            {
                 _context.Add(carrier);
            }
            else
            {
                await Task.Run(() => existedCarrier.CopyPropertyValues(carrier));
            }

            await _context.CompleteAsync();
            return true;
        }

        public async Task<Carrier> GetCarrierByIdAsync(int carrierId)
        {
            return await _context.SingleOrDefaultAsync(x => x.Id == carrierId);
        }

        public async Task<bool> DeleteCarrierAsync(int id)
        {
            var carrier = await GetCarrierByIdAsync(id);
            if (carrier != null)
            {
                _context.Remove(carrier);
                await _context.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Carrier>> GetAvailableCarriersAsync(User user)
        {
            return await _context.GetAvailableCarriersAsync(user);
        }

        public async Task<IEnumerable<Carrier>> GetMyCarriersAsync(User user)
        {
            return await _context.GetCarriersAsync(user);
        }


        public async Task<bool?> ChangeStatusCarrierAsync(int carrierId)
        {
            return await _context.ChangeStatusCarrierAsync(carrierId);
        }

        public async Task<IEnumerable<Carrier>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }
    }
}