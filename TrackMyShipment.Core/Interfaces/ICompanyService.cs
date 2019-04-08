﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<bool> PutCompany(string companyName, User user);
    }
}
