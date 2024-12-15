namespace airport_frontoffice.Models
{
    public class ListeReservation: Reservation
    {
        public int Payee {  get; set; }
        public DateTime DateDepart {  get; set; }
        public string Depart { get; set; }
        public string Arrive { get; set; }
    }
}
