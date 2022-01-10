using System.ComponentModel.DataAnnotations;

namespace CodingChallengeWeb.Models
{
    public class Playload
    {
        [Required]
        public int? load { get; set; }
        public Fuels fuels { get; set; }
        public List<Powerplant> powerplants { get; set; }
    }
}
