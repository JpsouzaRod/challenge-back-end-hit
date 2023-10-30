using StarWarsChallenge.Domain.Core.Models.Request;
using StarWarsChallenge.Domain.Core.Models.Response;
using System.Xml.Linq;

namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetUsecase
    {
        public BaseResponse AddPlanet(PlanetRequest planet);
        public BaseResponse ListPlanets();
        public BaseResponse FindPlanetById(int id);
        public BaseResponse FindPlanetByName(string name);
        public BaseResponse RemovePlanetById(int id);
    }
}
