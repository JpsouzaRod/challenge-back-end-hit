using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models;
using System.Numerics;

namespace StarWarsChallenge.Domain.Application.Usecase
{
    public class PlanetUsecase : IPlanetUsecase
    {
        public PlanetUsecase(IPlanetRepository _repository, IPlanetService _service)
        {
            repository = _repository;
            service = _service;
        }

        readonly IPlanetRepository repository;
        readonly IPlanetService service;

        public void AddPlanet(PlanetRequest planet)
        {
            repository.AddPlanet(planet);   
        }

        public PlanetResponse FindPlanetById(int id)
        {
            var result = repository.FindPlanetById(id);

            var response = new PlanetResponse(result);
            response.Occurrences = service.GetPlanetAppearances(response.Name);

            return response;
        }

        public PlanetResponse FindPlanetByName(string name)
        {
            var result = repository.FindPlanetByName(name);

            var response = new PlanetResponse(result);
            response.Occurrences = service.GetPlanetAppearances(response.Name);

            return response;
        }

        public IList<PlanetResponse> ListPlanets()
        {
            var planets = repository.ListPlanets();
            var result = new List<PlanetResponse>();

            foreach (var planet in planets)
            {
                var response = new PlanetResponse(planet);
                response.Occurrences = service.GetPlanetAppearances(response.Name);

                result.Add(response);
            }   

            return result;
        }

        public void RemovePlanetById(int id)
        {
            repository.RemovePlanet(id);
        }
    }
}
