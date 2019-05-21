namespace TrackMyShipment.Repository.Models
{
    public class Objective
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public bool Status { get; set; }
        public string Cost { get; set; }
        public int? carrierId { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}