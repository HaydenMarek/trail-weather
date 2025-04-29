# :globe_with_meridians: [TrailWeather.app](https://trailweather.app/) 

is designed to help users find weather forecasts for bike parks and trails within a specified range of a location. It integrates geolocation, location search, and weather data for an interactive and informative experience.

## Project Structure
- **trail-weather-api**
  - Exposes endpoints for weather forecasts and trail/bike park data.

- **trail-weather-frontend**
    - Provides a user interface for searching locations, viewing weather forecasts, and interacting with the API.
      
- **trail-weather-data-access**
  - Data access layer using Entity Framework Core.
  
- **trail-weather-data-loader**
  - Console application for importing and processing external data (e.g., KML files for trail locations).
  - Used for initial data population and updates to the database.
