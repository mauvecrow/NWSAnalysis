﻿using Microsoft.Net.Http.Headers;
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

    async Task<StationDto?> INwsService.GetStation(string stationId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = prepareMessage(baseUri);
        var responseMessage = await httpClient.SendAsync(requestMessage);
        if (responseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<StationDto>(contentStream);
        }
        return default;
    }

    async Task<Observations?> INwsService.GetStationObservations(string stationId)
    {
        throw new NotImplementedException();
    }

    async Task<StationsDto?> INwsService.GetStations(string state)
    {
        throw new NotImplementedException();
    }

    private HttpRequestMessage prepareMessage(string uri)
    {
        return new HttpRequestMessage(HttpMethod.Get, baseUri)
        {
            Headers =
            {
                {HeaderNames.Accept, "application/geo+json"},
                {HeaderNames.ContentType, "application/json"},
                {HeaderNames.UserAgent, "bradley.quangson@gmail.com"}
            }
        };
    }
}