using System.Runtime.InteropServices;
using trail_weather_api.Services;
using trail_weather_api.Services.Interfaces;
using trail_weather_data_access.Enums;
using trail_weather_data_access.Repositories;
using trail_weather_data_access.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var secretProvider = config.Providers.First();
string? secretPass = Environment.GetEnvironmentVariable("ConnectionStrings_DefaultConnection");

if (secretPass is null)
    secretProvider.TryGet("ConnectionString", out secretPass);

if (secretPass is null)
    throw new ArgumentNullException("Connection string is empty", secretPass);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISportCenterRepository>(provider => new SportCenterRepository(secretPass, RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DbProviders.SqlServer : DbProviders.MySql));

builder.Services.AddHttpClient<IForecastService, ForecastService>(client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Ok("Minimal api controller result."));
app.Run();
