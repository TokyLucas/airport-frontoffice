﻿namespace airport_frontoffice.Models
{
    public class VolDetails : Vol
    {
        public Avion Avion { get; set; }
        public Airport AirportDepartDetails {  get; set; }
        public Airport AirportArriveDetails { get; set; }
    }
}
