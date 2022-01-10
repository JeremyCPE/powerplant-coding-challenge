using System.ComponentModel.DataAnnotations;

namespace CodingChallengeWeb.Models
{
    public class Powerplant
    {
        [Required]
        public string? name { get; set; }
        [Required]
        public string? type { get; set; }
        [Required]
        public double? efficiency { get; set; }
        public int pmin { get; set; }
        [Required]
        public int pmax { get; set; }
        public double cost { get; set; }
    }
}
