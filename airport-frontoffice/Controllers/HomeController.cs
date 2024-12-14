using System.Diagnostics;
using airport_frontoffice.Helpers;
using airport_frontoffice.Models;
using airport_frontoffice.Services;
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
        public IActionResult RechercheVol(string depart, string arrive, string date)
        {
            List<Vol> vols = new List<Vol>();
            try
            {
                vols = _volService.FindVol(depart, arrive, date);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
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
