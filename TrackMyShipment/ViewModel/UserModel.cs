using System.ComponentModel.DataAnnotations;

namespace TrackMyShipment.ViewModel
{
    public class UserModel
    {
        [EmailAddress(ErrorMessage = "Invalid address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 11,
            ErrorMessage = "The length of the string must be from 11 to 50 characters")]
        [Phone(ErrorMessage = "Invalid number")]
        public string Phone { get; set; }
    }
}