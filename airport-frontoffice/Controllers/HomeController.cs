using System.Diagnostics;
using airport_frontoffice.Helpers;
using airport_frontoffice.Models;
using airport_frontoffice.Services;
using airport_frontoffice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace airport_frontoffice.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly VolService _volService;

        public HomeController(ILogger<HomeController> logger, VolService volService)
        {
            _logger = logger;
            _volService = volService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RechercheVol(string depart, string arrive, string dateDepart, string dateRetour, string allerRetour)
        {
            RechercheVol vols = new RechercheVol();
            List<VolDetails> aller = new List<VolDetails>();
            List<VolDetails> retour = new List<VolDetails>();
            try
            {
                aller = _volService.FindVol(depart, arrive, dateDepart);
            }
            catch (Exception ex)
            {
                aller = new List<VolDetails>();
                ViewData["Error"] = ex.Message;
            }
            finally
            {
                vols.Allers = aller;
            }

            if(allerRetour == "true")
            {
                try
                {
                    retour = _volService.FindVol(arrive, depart, dateRetour);
                }
                catch (Exception ex)
                {
                    retour = new List<VolDetails>();
                    ViewData["Error"] = ex.Message;
                }
                finally
                {
                    vols.Retours = retour;
                }
            }

            return PartialView("_ResultatRechercheVol", vols);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
