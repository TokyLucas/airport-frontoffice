using airport_frontoffice.Models;
using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.ViewModels
{
    public class PaiementDetails
    {
        public int ReservationId { get; set; }
        public List<VolDetails> Vol { get; set; }
        public List<ReservationDetails> Reservations { get; set; }
        [Required]
        public Paiement Paiement { get; set; }
    }
}
