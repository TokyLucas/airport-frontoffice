using airport_frontoffice.Models;
using airport_frontoffice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace airport_frontoffice.Controllers
{
    public class AirportController : Controller
    {
        private readonly AirportService _airpotService;

        public AirportController(AirportService airpotService)
        {
            _airpotService = airpotService;
        }
        // GET: Airport
        public ActionResult Index()
        {
            return View();
        }

        [Route("api/[controller]/Airports")]
        public string FindByVille(string ville)
        {
            List<Airport> airports = new List<Airport>();
            try
            {
                airports = _airpotService.FindByVille(ville);
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(ex.Message);
            }
            return JsonSerializer.Serialize(airports);
        }

        // GET: Airport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Airport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Airport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Airport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Airport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Airport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Airport/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
