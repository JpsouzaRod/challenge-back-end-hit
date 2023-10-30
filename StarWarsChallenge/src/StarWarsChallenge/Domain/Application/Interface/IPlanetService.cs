using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;

namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetService
    {
        int GetPlanetAppearancesByName(string planet);
        List<Planet> GetPlanetList();
    }
}
