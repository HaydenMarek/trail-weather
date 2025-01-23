using trail_weather_api.Services;
using trail_weather_api.Services.Interfaces;
using trail_weather_data_access.Repositories;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var secretProvider = config.Providers.First();
secretProvider.TryGet("ConnectionString", out var secretPass);

if (secretPass is null)
    throw new ArgumentNullException("Connection string is empty", secretPass);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISportCenterRepository>(provider => new SportCenterRepository(secretPass));
builder.Services.AddTransient<IForecastService, ForecastService>();

builder.Services.AddHttpClient<IForecastService, ForecastService>(client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok("Minimal api controller result.")); 

app.Run();
