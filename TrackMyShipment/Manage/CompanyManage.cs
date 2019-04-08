using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

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
            var result = await _companyService.PutCompany(companyName, user);
            if (result) return true;
             return false;
        }
    }
}

