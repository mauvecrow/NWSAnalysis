using Microsoft.AspNetCore.Mvc;
using NWSAnalysis.Server.Services;
using NWSAnalysis.Shared.Models;

namespace NWSAnalysis.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IApiService _apiService;

    public ForecastController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet("{state}")]
    public async Task<ForecastDetails> GetStationsApi(string state)
    {
        return await _apiService.GetStationNames(state);

    }

    [HttpGet("readings/{stationId}")]
    public async Task<Dictionary<DateTime, float?>> GetForecast(string stationId)
    {
        return await _apiService.GetStationForecasts(stationId);
    }
}
