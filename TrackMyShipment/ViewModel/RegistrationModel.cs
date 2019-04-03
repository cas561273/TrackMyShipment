using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrackMyShipment.Repository.ViewModel;

namespace TrackMyShipment.ViewModel
{
    public class RegistrationModel:UserModel
    {
        public string CompanyName { get; set; }
    }
}
