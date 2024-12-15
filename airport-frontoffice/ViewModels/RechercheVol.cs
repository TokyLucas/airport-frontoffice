using airport_frontoffice.Models;
using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.ViewModels
{
    public class RechercheVol
    {
        public List<VolDetails> Allers { get; set; }
        public List<VolDetails> Retours { get; set; }

    }
}
