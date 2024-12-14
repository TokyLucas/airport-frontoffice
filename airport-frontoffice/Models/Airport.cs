namespace airport_frontoffice.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string Timezone { get; set; }
    }
}
