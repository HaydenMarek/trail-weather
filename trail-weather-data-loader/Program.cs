using Microsoft.Extensions.Configuration;
using SharpKml.Dom;
using SharpKml.Engine;
using System.Text;
using trail_weather_data_access;
using trail_weather_data_access.Models;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var secretProvider = config.Providers.First();
secretProvider.TryGet("ConnectionString", out var secretPass);

if (secretPass is null)
    throw new ArgumentNullException("Connection string is empty", secretPass);
string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
string kmlFilePath = Path.Combine(projectDirectory, "TRAIL HUNTER.kml");
string kmlFileContents = "";
try
{
    kmlFileContents = File.ReadAllText(kmlFilePath);
}
catch (IOException e)
{
    Console.WriteLine($"An error occurred: {e.Message}");
    return;
}

if (kmlFileContents is "")
{
    Console.WriteLine("Kml file has no content");
    return;
}
KmlFile file;
using (var stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(kmlFileContents)))
{
    file = KmlFile.Load(stream);
}

using (var db = new TrailWeatherDbContext(secretPass))
{
    db.Database.EnsureCreated();
    var allTypes = file.Root.Flatten().OfType<Folder>().ToList();
    foreach (var type in allTypes)
    {
        foreach (var sportCenterItem in type.Features)
        {
            SportCenterType sportCenterType;
            SportCenterType? findCenterType = db.SportCenterType.Where(sct => sct.Name == type.Name).SingleOrDefault();
            if (findCenterType is null)
                sportCenterType = new SportCenterType { Name = type.Name };
            else
                sportCenterType = findCenterType;

            var geoData = new GeoData
            {
                Lat = Math.Round(((Point)((Placemark)sportCenterItem).Geometry).Coordinate.Latitude, 2),
                Lon = Math.Round(((Point)((Placemark)sportCenterItem).Geometry).Coordinate.Longitude, 2)
            };

            var sportCenter = new SportCenter { Name = sportCenterItem.Name, GeoData = geoData, SportCenterType = sportCenterType };

            db.SportCenter.Add(sportCenter);
            db.SaveChanges();
        }
    }
}