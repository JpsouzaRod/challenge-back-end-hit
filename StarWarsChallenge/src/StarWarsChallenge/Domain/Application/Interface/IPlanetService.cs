using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;

namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetService
    {
        Task<Planet> GetPlanetByName(string planet);
        Task<List<Planet>> GetPlanetList();
    }
}
