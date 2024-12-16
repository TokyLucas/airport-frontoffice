using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class ReservationDetails
    {
        public int VolId { get; set; }
        public int ClientId { get; set; }
        public int TarifPersonneId { get; set; }
        public int ReservationId { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlaceEconomique { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlaceAffaire { get; set; }
        [Required]
        [Range(0, 99)]
        public int NbPlacePremiere { get; set; }
        public string Personne {  get; set; }
        public int Payee { get; set; }
        public decimal TotalEconomique { get; set; }
        public decimal TotalAffaire { get; set; }
        public decimal TotalPremiere { get; set; }
    }
}
