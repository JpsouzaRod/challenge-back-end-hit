using StarWarsChallenge.Domain.Core.Models;

namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetRepository
    {
        public void AddPlanet(PlanetRequest planet);
        public IList<PlanetDTO> ListPlanets();
        public PlanetDTO FindPlanetById(int id);
        public PlanetDTO FindPlanetByName(string name);
        public void RemovePlanet(int id);
    }
}
