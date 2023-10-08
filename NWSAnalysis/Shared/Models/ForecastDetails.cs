using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NWSAnalysis.Shared.Models
{
    public class ForecastDetails
    {
        [JsonPropertyName("stationNames")]
        public Dictionary<string, string>? StationNames { get; set; }
        public string? StateCode { get; set; }
        public string? StationChoice { get; set; }
        public Dictionary<DateTime, float?>? TemperatureReadings { get; set;}
    }
}
