﻿using System.Threading.Tasks;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Constant;
using TrackMyShipment.Repository.Helper;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;
using Role = TrackMyShipment.Repository.Constant.Role;

namespace TrackMyShipment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _context;

        public UserService(IUserRepository context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.GetByEmail(email);
        }

        public async Task<User> Get(User user)
        {
            return await _context.UserExists(user);
        }

        public async Task<bool> Create(User user, string companyName)
        {
            var temp = await _context.GetByEmail(user.Email);
            if (temp != null) return false;
            await _context.AddAsync(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password),
                RoleId = await _context.GetRoleId(Role.CUSTOMER),
                SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE)
            });
            await _context.CompleteAsync();
            await _context.PutCompany(companyName, user.Email);
            return true;
        }

        public async Task PutCarrier(User carrier)
        {
            var user = await _context.UserExists(carrier);
            if (user == null)
            {
                carrier.RoleId = await _context.GetRoleId(Role.CARRIER);
                carrier.SubscriptionId = await _context.GetSubscribeId(Subscribe.FREE);
                carrier.Password = PasswordHelper.CalculateHashedPassword(carrier.Email, carrier.Password);
                await _context.AddAsync(carrier);
            }
            else
            {
                user.FirstName = carrier.FirstName;
                user.LastName = carrier.LastName;
                user.Phone = carrier.Phone;
            }
            await _context.CompleteAsync();
        }
    }
}