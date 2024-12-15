namespace airport_frontoffice.Models
{
    public class Vol
    {
        public int VolId { get; set; }
        public int AvionId { get; set; }
        public DateTime DateDepart { get; set; }
        public int AirportDepart { get; set; }
        public int AirportArrive { get; set; }
        public decimal PrixDeBaseEconomique { get; set; }
        public decimal PrixDeBaseAffaire { get; set; }
        public decimal PrixDeBasePremiere { get; set; }
    }
}
