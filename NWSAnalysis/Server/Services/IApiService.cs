using NWSAnalysis.Shared.Models;

namespace NWSAnalysis.Server.Services;

public interface IApiService
{
    Task<ForecastDetails?> GetStationNames(string stateCode);
    Task<Dictionary<DateTime, float?>> GetStationForecasts(string stationId);

}
