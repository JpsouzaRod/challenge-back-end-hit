using Microsoft.AspNetCore.Rewrite;
using StarWarsChallenge.Adapter.StarWarsApi.Models;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;

namespace StarWarsChallenge.Adapter.StarWarsApi.Service
{
    public class PlanetService : IPlanetService
    {
        private HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        }; 

        public int GetPlanetAppearancesByName(string planet)
        {
            var number = 0;

            try
            {
                var result = client.GetFromJsonAsync<ApiResponse>($"planets/?search={planet}").Result;

                if (result != null && result.count > 0)
                {
                    number = result.results.First().films.Count();
                }

                return number;
            }
            catch (Exception ex)
            {
                return number;
            }

        }

        public List<Planet> GetPlanetList()
        {
            List<Planet> list = new List<Planet>();

            try
            {
                var result = client.GetFromJsonAsync<ApiResponse>($"planets/").Result;

                if (result != null && result.count > 0)
                {
                    list = result.results.ToList<Planet>();
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
