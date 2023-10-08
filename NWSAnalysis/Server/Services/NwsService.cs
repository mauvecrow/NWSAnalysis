using Microsoft.Net.Http.Headers;
using NWSAnalysis.Shared.Models;
using System.Text.Json;

namespace NWSAnalysis.Server.Services;

public class NwsService : INwsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUri = @"https://api.weather.gov/stations";

    public NwsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    async Task<StationsDto?> INwsService.GetStations(string state)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage($"{baseUri}?state={state}");
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<StationsDto?>(contentStream);

        }
        return default;
    }

    async Task<StationDto?> INwsService.GetStation(string stationId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage($"{baseUri}/{stationId}");
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<StationDto?>(contentStream);
        }
        return default;
    }

    async Task<Observations?> INwsService.GetStationObservations(string stationId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage($"{baseUri}/{stationId}/observations");
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Observations?>(contentStream);
        }
        return default;
    }

    public async Task<string?> GetRawStation(string stationId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage($"{baseUri}/{stationId}");
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            //using var contentStream = await responseMessage.Content.ReadAsStreamAsync();

            return await responseMessage.Content.ReadAsStringAsync();
        }
        return default;
    }

    public async Task<string?> GetRawObservation(string stationId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage($"{baseUri}/{stationId}/observations");
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            //using var contentStream = await responseMessage.Content.ReadAsStreamAsync();

            return await responseMessage.Content.ReadAsStringAsync();
        }
        return default;
    }

    private HttpRequestMessage prepareMessage(string uri)
    {
        return new HttpRequestMessage(HttpMethod.Get, uri)
        {
            Headers =
            {
                {HeaderNames.Accept, "application/geo+json"},
                {HeaderNames.UserAgent,  "NswServiceApp/v1.0 (http://localhost; bquangson@gmail.com)"}
            }
        };
    }
}
