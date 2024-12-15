namespace airport_frontoffice.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int NumeroReservation { get; set; }
        public int ClientId { get; set; }
        public int VolId { get; set; }
        public DateTime DateReservation { get; set; }
    }
}
