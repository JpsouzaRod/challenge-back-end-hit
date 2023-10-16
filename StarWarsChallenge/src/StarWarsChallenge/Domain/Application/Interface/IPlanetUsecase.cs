using StarWarsChallenge.Domain.Core.Models;
using System.Xml.Linq;

namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetUsecase
    {
        public void AddPlanet(PlanetRequest planet);
        public IList<PlanetResponse> ListPlanets();
        public PlanetResponse FindPlanetById(int id);
        public PlanetResponse FindPlanetByName(string name);
        public void RemovePlanetById(int id);
    }
}
