using airport_frontoffice.Helpers;
using airport_frontoffice.Models;
using airport_frontoffice.Services;
using Microsoft.AspNetCore.Mvc;

namespace airport_frontoffice.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login([FromQuery] string action)
        {
            ViewData["Message"] = null;
            if (action == "SignedIn") ViewData["Message"] = "Compte créée. Vous Pouvez vous connectez.";
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("Email, Motdepasse")] ClientLogin client)
        {

            if (ModelState.IsValid)
            {
                Client clientConnecte = _clientService.Login(client.Email, client.Motdepasse);

                if (clientConnecte == null)
                {
                    ViewData["error"] = "Email ou mot de passe invalide";
                    return View(client);
                }
                HttpContext.Session.SetObjectAsJson("Client", clientConnecte);

                // Auth par claim
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, client.ClientId.ToString())
                //};

                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Client");
            return RedirectToAction("Login", "Client");
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn([Bind("Nom, Prenom, Email, Motdepasse, Telephone, Adresse")] Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int clientConnecte = _clientService.SignIn(client);
                }
                catch (Exception ex)
                {
                    ViewData["error"] = ex.Message;
                    return View(client);
                }
                //return RedirectToAction("Login", "Client", new { action = "SignedIn" });
                return Redirect(Url.Action("Login", "Client") + "?action=SignedIn");
            }

            return View(client);
        }
    }
}
