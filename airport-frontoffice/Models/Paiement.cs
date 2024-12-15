using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class Paiement
    {
        public int PaiementId { get; set; }
        public int ReservationId { get; set; }
        public int DatePaiement { get; set; }
        [Range(12,12)]
        public int NumeroCarteBancaire { get; set; }
    }
}
