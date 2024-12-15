using airport_frontoffice.Models;

namespace airport_frontoffice.ViewModels
{
    public class PaiementDetails
    {
        public int ReservationId { get; set; }
        public List<VolDetails> Vol { get; set; }
        public List<ReservationDetails> Reservations { get; set; }
        public Paiement Paiement { get; set; }
    }
}
