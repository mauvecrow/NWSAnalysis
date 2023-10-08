using Microsoft.AspNetCore.Mvc;
using NWSAnalysis.Server.Services;

namespace NWSAnalysis.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TempController : ControllerBase
{
    private readonly IApiService _apiService;

    public TempController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet("{stationId}")]
    public async Task<Dictionary<DateTime, float?>> GetForecast(string stationId)
    {
        return await _apiService.GetStationForecasts(stationId);
    }
}
