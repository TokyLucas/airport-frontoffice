using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class Paiement
    {
        public int PaiementId { get; set; }
        public int ReservationId { get; set; }
        public int DatePaiement { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string NumeroCarteBancaire { get; set; }
    }
}
