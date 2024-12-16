using airport_frontoffice.Models;

namespace airport_frontoffice.ViewModels
{
    public class ReservationForm
    {
        public int VolId { get; set; }
        public List<VolDetails> Vol {  get; set; }
        public PlaceReservation PlacesAdulte {  get; set; }
        public PlaceReservation PlacesEnfant { get; set; }
        public PlaceReservation PlacesBebe { get; set; }
    }
}
