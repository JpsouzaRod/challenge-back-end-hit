using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models;
using System.Numerics;

namespace StarWarsChallenge.Domain.Application.Usecase
{
    public class PlanetUsecase : IPlanetUsecase
    {
        public PlanetUsecase(IPlanetRepository _repository)
        {
            repository = _repository;
        }

        readonly IPlanetRepository repository;

        public void AddPlanet(PlanetRequest planet)
        {
            repository.AddPlanet(planet);   
        }

        public PlanetResponse FindPlanetById(int id)
        {
            var result = repository.FindPlanetById(id);

            var response = new PlanetResponse(result);
            
            return response;
        }

        public PlanetResponse FindPlanetByName(string name)
        {
            var result = repository.FindPlanetByName(name);

            var response = new PlanetResponse(result);

            return response;
        }

        public IList<PlanetResponse> ListPlanets()
        {
            var result = repository.ListPlanets();
            var response = new List<PlanetResponse>();
            return response;
        }

        public void RemovePlanetById(int id)
        {
            repository.RemovePlanet(id);
        }
    }
}
