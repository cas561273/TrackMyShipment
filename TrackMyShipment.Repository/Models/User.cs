using System.ComponentModel.DataAnnotations;

namespace TrackMyShipment.Repository.Models
{
    public class User
    {
        [ScaffoldColumn(false)] public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 11,
            ErrorMessage = "The length of the string must be from 11 to 50 characters")]
        [Phone]
        public string Phone { get; set; }

        [EmailAddress] public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the string must be from 8 to 50 characters")]
        public string Password { get; set; }

        public int? SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}