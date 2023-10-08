using NWSAnalysis.Shared.Models;

namespace NWSAnalysis.Server.Services;

public class ApiService : IApiService
{
    private readonly INwsService _service;

    public ApiService(INwsService service)
    {
        _service = service;
    }

    public async Task<ForecastDetails?> GetStationNames(string stateCode)
    {

        var dto = await _service.GetStations(stateCode);
        if(dto != null)
        {
            var details = new ForecastDetails();
            var dictionary = new Dictionary<string, string>();
            foreach (var f in dto.features)
            {
                dictionary.Add(f.properties.stationIdentifier, f.properties.name);
            }
            details.StationNames = dictionary;
            details.StateCode = stateCode;
            return details;
        }
        return default;

    }

    public async Task<Dictionary<DateTime, float?>> GetStationForecasts(string stationId)
    {
        var dto = await _service.GetStationObservations(stationId);
        if(dto != null)
        {
            var readings = new Dictionary<DateTime, float?>();
            foreach (var f in dto.features)
            {
                readings.Add(f.properties.timestamp, f.properties.temperature.value);
            }
            return readings;
        }
        return default;
    }


}
