using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Nom requis.")]
        public string? Nom { get; set; }
        [Required(ErrorMessage = "Prenom requis.")]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "Email requis.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe requis.")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Doit être >= à 8 caractères")]
        public string Motdepasse { get; set; }
        [Required(ErrorMessage = "Adresse requis.")]
        public string? Adresse { get; set; }
        [Required(ErrorMessage = "Telephone requis.")]
        public string? Telephone { get; set; }

    }
}
