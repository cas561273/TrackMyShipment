namespace TrackMyShipment.ViewModel
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
        public int? UsersId { get; set; }

    }
}