using StarWarsChallenge.Adapter.StarWarsApi.Models;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;
using System.Text.Json;

namespace StarWarsChallenge.Adapter.StarWarsApi.Service
{
    public class PlanetService : IPlanetService
    {
        public PlanetService(IPlanetCache _cache)
        {
            cache = _cache;
        }

        readonly IPlanetCache cache;
        private readonly string key = "List";

        private HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        }; 

        public async Task<Planet> GetPlanetByName(string planet)
        {
            var planetResult = new Planet()
            {
                films = new List<string> { }
            };

            try
            {
                var dataPlanet = await cache.GetAsync(planet);

                if (dataPlanet != null)
                {
                    return JsonSerializer.Deserialize<Planet>(dataPlanet);
                }

                var result = client.GetFromJsonAsync<ApiResponse>($"planets/?search={planet}").Result;

                if (result != null && result.count > 0)
                {
                    planetResult = result.results.First();
                    await cache.SetAsync(planet, JsonSerializer.Serialize(planetResult));
                }


                return planetResult;
            }
            catch (Exception ex)
            {
                return planetResult;
            }

        }

        public async Task<List<Planet>> GetPlanetList()
        {
            List<Planet> list = new List<Planet>();

            try
            {
                var dataPlanet = await cache.GetAsync(key);

                if (dataPlanet != null)
                {
                    return JsonSerializer.Deserialize<List<Planet>>(dataPlanet);
                }

                var result = client.GetFromJsonAsync<ApiResponse>($"planets/").Result;

                if (result != null && result.count > 0)
                {
                    list = result.results.ToList();
                    await cache.SetAsync(key, JsonSerializer.Serialize(list));
                }

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }

        }

    }
}
