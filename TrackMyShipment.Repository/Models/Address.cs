namespace TrackMyShipment.Repository.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
        public int? UsersId { get; set; }
        public User Users { get; set; }
    }
}