using System.Text.Json.Serialization;

namespace NWSAnalysis.Shared.Models;
public class Observations
{
    public string type { get; set; }
    public Feature[] features { get; set; }


    public class Feature
    {
        public string id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("@id")]
        public string id { get; set; }
        [JsonPropertyName("@type")]
        public string type { get; set; }
        public Elevation elevation { get; set; }
        public string station { get; set; }
        public DateTime timestamp { get; set; }
        public string rawMessage { get; set; }
        public string textDescription { get; set; }
        public string icon { get; set; }
        public Temperature temperature { get; set; }

        public Maxtemperaturelast24hours maxTemperatureLast24Hours { get; set; }
        public Mintemperaturelast24hours minTemperatureLast24Hours { get; set; }

    }

    public class Elevation
    {
        public string unitCode { get; set; }
        public float value { get; set; }
    }

    public class Temperature
    {
        public string unitCode { get; set; }
        public float? value { get; set; }
        public string qualityControl { get; set; }
    }

    


  

    public class Maxtemperaturelast24hours
    {
        public string unitCode { get; set; }
        public object value { get; set; }
    }

    public class Mintemperaturelast24hours
    {
        public string unitCode { get; set; }
        public object value { get; set; }
    }

    

}