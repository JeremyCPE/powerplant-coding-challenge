using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodingChallengeWeb.Models
{
    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        [Required]
        public double? gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        [Required]
        public double? kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public double co2 { get; set; }
        [JsonPropertyName("wind(%)")]
        [Required]
        public int? wind { get; set; }

    }
}