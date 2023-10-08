using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;
using NWSAnalysis.Server.Services;
using NWSAnalysis.Shared.Models;
using System.Net.Http;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// adding services I made
builder.Services.AddHttpClient();
builder.Services.AddScoped<INwsService, NwsService>();
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

//minimal api style for testing http calls
app.MapGet("/test1/{state}", Test1);
app.MapGet("/test2/{stationId}", Test2);
app.MapGet("/test3/{stationId}", Test3);
app.MapGet("/test2a/{stationId}", Test2a);
app.MapGet("/test3a/{stationId}", Test3a);
app.Run();

static async Task<StationsDto?> Test1(string state, INwsService service)
{
    return await service.GetStations(state);
}

static async Task<StationDto?> Test2(string stationId, INwsService service)
{
    return await service.GetStation(stationId);
}

static async Task<string?> Test2a(string stationId, INwsService service)
{
    return await ((NwsService)service).GetRawStation(stationId);
}

static async Task<Observations?> Test3(string  stationId, INwsService service)
{
    return await service.GetStationObservations(stationId);
}

static async Task<string?> Test3a(string stationId, INwsService service)
{
    return await ((NwsService)service).GetRawObservation(stationId);
}