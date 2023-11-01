using StarWarsChallenge.Adapter.StarWarsApi.Models;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;
using System.Text.Json;

namespace StarWarsChallenge.Adapter.StarWarsApi.Service
{
    public class PlanetService : IPlanetService
    {
        public PlanetService(HttpClient _client, IPlanetCache _cache)
        {
            cache = _cache;
            client = _client;
        }

        private readonly IPlanetCache cache;
        private readonly string key = "List";
        private readonly HttpClient client;

        public async Task<Planet> GetPlanetByName(string planet)
        {
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
                    var planetResult = result.results.First();
                    await cache.SetAsync(planet, JsonSerializer.Serialize(planetResult));

                    return planetResult;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
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
