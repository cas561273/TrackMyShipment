namespace TrackMyShipment.Repository.Models
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public bool Status { get; set; }
        public long Cost { get; set; }
    }
}