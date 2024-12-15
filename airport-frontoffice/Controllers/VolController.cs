using airport_frontoffice.Models;
using airport_frontoffice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace airport_frontoffice.Controllers
{
    public class VolController : Controller
    {
        private readonly VolService _volService;

        public VolController(VolService volService)
        {
            _volService = volService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/[controller]/Vols")]
        public string Find(string depart, string arrive, string date)
        {
            List<VolDetails> vols = new List<VolDetails>();
            try
            {
                vols = _volService.FindVol(depart, arrive, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonSerializer.Serialize(vols);
        }
    }
}
