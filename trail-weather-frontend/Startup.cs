using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using trail_weather_frontend.Services;
using trail_weather_frontend.Services.Interfaces;

namespace trail_weather_frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient<IApiCaller, ApiCaller>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:7168/WeatherForecast/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "TrailWeatherApp");
            });
            services.AddGeolocationServices();
            services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri("https://nominatim.openstreetmap.org/"),
                    DefaultRequestHeaders = { { "Accept", "application/json" }, { "User-Agent", "TrailWeatherApp" } }
                });
            services.AddBlazorBootstrap();
            services.AddScoped<ILocationSearchService, LocationSearchService>();
        }        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
