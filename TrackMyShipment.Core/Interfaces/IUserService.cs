using System.Collections.Generic;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Interfaces
{
    public interface IUserService
    {
        User GetByEmail(string email);
        User Get(User person);
        bool Create(User person,string companyName);
        void PutCarrier(User carrier);
    }
}