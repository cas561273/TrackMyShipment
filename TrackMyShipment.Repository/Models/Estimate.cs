using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMyShipment.Repository.Models
{
   public class Estimate
    {
        public string Status { get; set; }
        public int? objectiveId { get; set; }
        public virtual Objective Objective { get; set; }
        public virtual User User { get; set; }
        public int? userId { get; set; }


    }
}
