using System.Text.Json;
using CodingChallengeWeb.Models;

namespace CodingChallengeWeb.Services
{
    public class ProductionPlanServices
    {
        public static List<Response> Calculate(Playload playload)
        {
            double? load = playload.load;
            if(load < 0)
            {
                throw new ArgumentException("load undefined or inferior at 0", nameof(playload.load));
            }
            if (playload.powerplants == null)
            {
                throw new ArgumentException("powerplants undefined", nameof(playload.powerplants));
            }
            if (playload.fuels == null)
            {
                throw new ArgumentException("fuels undefined", nameof(playload.fuels));
            }

            List<Response> response = new List<Response>();

            foreach(var powerplant in playload.powerplants)
            {
                switch(powerplant.type)
                {
                    case "windturbine":
                        powerplant.cost = 0;
                        break;
                    case "gasfired":
                        powerplant.cost = powerplant.efficiency > 0 ? Math.Round(playload.fuels.gas / powerplant.efficiency,1) : 0;
                        break;
                    case "turbojet":
                        powerplant.cost = powerplant.efficiency > 0 ? Math.Round(playload.fuels.kerosine / powerplant.efficiency,1) : 0;
                        break;
                    default:
                        throw new ArgumentException($"type {powerplant.type} undefined or unknown", nameof(powerplant.type));
                }
            }

            List<Powerplant> powerplants = playload.powerplants.OrderByDescending(d => d.efficiency).ThenBy(k => k.cost).ToList();

            // Set minimum foreach powerplant
            foreach (var powerplant in powerplants)
            {
                response.Add(new(powerplant.name ?? "", powerplant.pmin));
            }
            
            // Determine power foreach powerplant (sorted by efficiency then by cost)
            foreach (var powerplant in powerplants)
            {
                double total = response.Sum(d => d.p);
                Response current = response.First(d => d.name == powerplant.name);
                double value;

                if (total >= load)
                    break;

                if (powerplant.type == "windturbine")
                {
                    value = Math.Round((double)playload.fuels.wind / 100 * powerplant.pmax, 1);
                    if (playload.fuels.wind <= 0 || value > load)
                    {
                        // Switch off powerplant
                        continue;
                    }
                    else
                    {
                        // Switch on powerplant
                        current.p = value;
                    }
                }
                else
                {
                    double neededValue = Math.Round(load.Value - total,1);
                    double maximalValue = powerplant.pmax;

                    if (neededValue > maximalValue)
                    {
                        value = maximalValue;
                    }
                    else
                    {
                        value = neededValue + current.p;
                    }
                    current.p = value;
                }

            }

            return response;
        }
    }
}
