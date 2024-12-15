using airport_frontoffice.Helpers;
using airport_frontoffice.Models;
using airport_frontoffice.Services;
using airport_frontoffice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace airport_frontoffice.Controllers
{
    public class ReservationController : Controller
    {
        private readonly VolService _volService;
        private readonly ReservationService _reservationService;
        private readonly PaiementService _paiementService;

        public ReservationController(VolService volService, ReservationService reservationService, PaiementService paiementService)
        {
            _volService = volService;
            _reservationService = reservationService;
            _paiementService = paiementService;
        }
        public IActionResult Reserver(int ID)
        {
            ReservationForm form = new ReservationForm();
            List<VolDetails> volDetails = new List<VolDetails>();
            try
            {
                volDetails = _volService.FindById(ID.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                form.VolId = ID;
                form.Vol = volDetails;
                form.PlacesAdulte = new PlaceReservation();
                form.PlacesEnfant = new PlaceReservation();
                form.PlacesBebe = new PlaceReservation();
            }
            return View(form);
        }
        [HttpPost]
        public IActionResult SubmitReservation(ReservationForm form)
        {
            List<VolDetails> volDetails = new List<VolDetails>();
            try
            {
                volDetails = _volService.FindById(form.VolId.ToString());
                form.Vol = volDetails;
                Reservation reservation = new Reservation();
                reservation.DateReservation = DateTime.Now;
                reservation.ClientId = HttpContext.Session.GetObjectFromJson<Client>("Client").ClientId;
                reservation.VolId = form.VolId;

                List<PlaceReservation> places = new List<PlaceReservation>();
                form.PlacesAdulte.TarifPersonneId = 1;
                form.PlacesEnfant.TarifPersonneId = 2;
                form.PlacesBebe.TarifPersonneId = 3;
                places.Add(form.PlacesAdulte);
                places.Add(form.PlacesEnfant);
                places.Add(form.PlacesBebe);

                int result = _reservationService.Reserver(reservation, places);
                ViewData["Message"] = "Reservé " + result;
            }
            catch (Exception ex)
            {
                throw ex;
                //ViewData["Message"] = ex.Message;
            }
            return View("Reserver", form);
        }

        public IActionResult Liste(string message)
        {
            List<ListeReservation> reservations = new List<ListeReservation>();
            try
            {
                int ID = HttpContext.Session.GetObjectFromJson<Client>("Client").ClientId;
                reservations = _reservationService.FindByClientId(ID.ToString());
                if (message == "PaiementEffectuee")
                    ViewData["message"] = "PaiementEffectuee";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(reservations);
        }

        public IActionResult Paiement(int ID)
        {
            PaiementDetails details = new PaiementDetails();
            List<Models.ReservationDetails> reservations = new List<Models.ReservationDetails>();
            List<VolDetails> volDetails = new List<VolDetails>();
            try
            {
                reservations = _reservationService.FindById(ID.ToString());
                foreach (ReservationDetails d in reservations)
                    volDetails = _volService.FindById(d.VolId.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                details.ReservationId = ID;
                details.Vol = volDetails;
                details.Reservations = reservations;
                details.Paiement = new Paiement();
            }
            return View(details);
        }
        [HttpPost]
        public IActionResult SubmitPaiement(PaiementDetails details)
        {
            List<Models.ReservationDetails> reservations = new List<Models.ReservationDetails>();
            List<VolDetails> volDetails = new List<VolDetails>();
            try
            {
                reservations = _reservationService.FindById(details.ReservationId.ToString());
                foreach(ReservationDetails d in reservations)
                    volDetails = _volService.FindById(d.VolId.ToString());

                details.Paiement.ReservationId = details.ReservationId;
                int result = _paiementService.Payer(details.Paiement);
                if(result > 0)
                    return Redirect(Url.Action("Liste", "Reservation") + "?message=PaiementEffectuee");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                details.Vol = volDetails;
                details.Reservations = reservations;
            }
            return View("Paiement", details);
        }
    }
}
