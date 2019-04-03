using System;
using System.ComponentModel.DataAnnotations;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.ViewModel
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Invalid address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the string must be from 8 to 50 characters")]
        public string Password { get; set; }

    }
}