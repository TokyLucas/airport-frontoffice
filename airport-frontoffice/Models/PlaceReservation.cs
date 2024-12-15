using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class PlaceReservation
    {
        public int ReservationId { get; set; }
        public int TarifPersonneId { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlaceEconomique { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlaceAffaire { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlacePremiere { get; set; }

    }
}
