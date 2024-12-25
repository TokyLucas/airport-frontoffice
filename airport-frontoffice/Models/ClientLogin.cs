using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class ClientLogin
    {

        [Required(ErrorMessage = "Email requis.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe requis.")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Doit être >= à 8 caractères")]
        public string Motdepasse { get; set; }
    }
}
