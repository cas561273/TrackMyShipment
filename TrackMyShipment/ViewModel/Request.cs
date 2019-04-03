using TrackMyShipment.Core.ViewModel;

namespace TrackMyShipment.ViewModel
{
    public class Request
    {
        public RequestState State { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}

