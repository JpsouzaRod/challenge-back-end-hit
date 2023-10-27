using StarWarsChallenge.Adapter.StarWarsApi.Models;
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

        public BaseResponse AddPlanet(PlanetRequest planet)
        {
            try
            {
                repository.AddPlanet(planet);
                return new BaseResponse()
                {
                    StatusOk = true,
                    Message = "Cadastro foi feito com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Message = "Não foi possivel realizar o cadastro."
                };
            }
        }

        public BaseResponse FindPlanetById(int id)
        {
            try
            {
                var result = repository.FindPlanetById(id);

                if (result == null) 
                    throw new Exception("O valor de busca não foi localizado.");
                
                var response = new PlanetResponse(result);
                response.Occurrences = service.GetPlanetAppearances(response.Name);

                return new BaseResponse()
                {
                    StatusOk = true,
                    Planets = new List<PlanetResponse>()
                    {
                        response
                    }
                };
            }
            catch
            {
                return new BaseResponse()
                {
                    Message = "Nenhum planeta foi localizado."
                };
            }

            
        }

        public BaseResponse FindPlanetByName(string name)
        {
            try
            {
                var result = repository.FindPlanetByName(name);

                if (result == null)
                    throw new Exception("O valor de busca não foi localizado.");

                var response = new PlanetResponse(result);
                response.Occurrences = service.GetPlanetAppearances(response.Name);

                return new BaseResponse()
                {
                    StatusOk = true,
                    Planets = new List<PlanetResponse>()
                    {
                        response
                    }
                };
            }
            catch
            {
                return new BaseResponse()
                {
                    Message = "Nenhum planeta foi localizado."
                };
            }

            
        }

        public BaseResponse ListPlanets()
        {
            try
            {
                var planets = repository.ListPlanets();

                if (planets == null || planets.Count() == 0)
                    throw new Exception("Nenhum planeta foi localizado.");

                var result = new List<PlanetResponse>();

                foreach (var planet in planets)
                {
                    var response = new PlanetResponse(planet);
                    response.Occurrences = service.GetPlanetAppearances(response.Name);

                    result.Add(response);
                }

                return new BaseResponse()
                {
                    StatusOk = true,
                    Planets = result
                };
            }
            catch
            {
                return new BaseResponse()
                {
                    Message = "Não foi possivel realizar esta consulta."
                };
            }
        }

        public BaseResponse RemovePlanetById(int id)
        {
            try
            {
                repository.RemovePlanet(id); 
                return new BaseResponse()
                {
                    StatusOk = true,
                    Message = "Planeta excluido com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Message = "Não foi possível concluir a exclusão do planeta."
                };
            }
            
        }
    }
}
