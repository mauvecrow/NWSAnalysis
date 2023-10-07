using NWSAnalysis.Shared.Models;
namespace NWSAnalysis.Server.Services;

public interface INwsService
{
    // Call /stations
    Task<StationsDto> GetStations(string state);
    // Call /stations/{id}
    Task<StationDto> GetStation(string stationId);
    // Call /stations/{id}/observations
    Task<Observations> GetStationObservations(string stationId);
}
