using Microsoft.AspNetCore.Rewrite;
using StarWarsChallenge.Adapter.StarWarsApi.Models;
using StarWarsChallenge.Domain.Application.Interface;

namespace StarWarsChallenge.Adapter.StarWarsApi.Service
{
    public class PlanetService : IPlanetService
    {
        private HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        }; 

        public int GetPlanetAppearances(string planet)
        {
            var number = 0;

            var result = client.GetFromJsonAsync<ApiResponse>($"planets/?search={planet}").Result;

            if  (result != null && result.count > 0) 
            {
                number = result.results.First().films.Count();
            }

            return number;
        }

    }
}
