namespace TrackMyShipment.Repository.Models
{
    public class Supplies
    {
        public int? UserId { get; set; }
        public int? CarrierId { get; set; }

        public virtual User User { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}