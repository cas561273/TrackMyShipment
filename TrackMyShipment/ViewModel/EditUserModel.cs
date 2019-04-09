using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyShipment.ViewModel
{
    public class EditUserModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 11, ErrorMessage = "The length of the string must be from 11 to 50 characters")]

        [Phone] public string Phone { get; set; }
    }
}
