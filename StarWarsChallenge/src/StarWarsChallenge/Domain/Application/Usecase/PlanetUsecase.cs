using StarWarsChallenge.Adapter.StarWarsApi.Models;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models;
using StarWarsChallenge.Domain.Core.Models.Request;
using StarWarsChallenge.Domain.Core.Models.Response;
using System;

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
            catch 
            {
                return new BaseResponse()
                {
                    Message = "Não foi possivel realizar o cadastro."
                };
            }
        }

        public BaseResponse FindPlanetById(int id)
        {
            var count = 0;

            try
            {
                var result = repository.FindPlanetById(id);

                if (result is null) 
                    throw new Exception("Nenhum planeta foi localizado.");

                var planetResult = service.GetPlanetByName(result.Name).Result;

                if (planetResult is not null)
                    count = planetResult.films.Count();

                var response = new PlanetResponse(result);
                response.Occurrences = count;

                var planet =  new BaseResponse()
                {
                    StatusOk = true,
                    Planets = new List<PlanetResponse>()
                    {
                        response
                    }
                };

                return planet;
            }
            catch(Exception ex)
            {
                return new BaseResponse()
                {
                    Message = ex.Message
                };
            }

            
        }

        public BaseResponse FindPlanetByName(string name)
        {
            var count = 0;

            try
            {
                var result = repository.FindPlanetByName(name);

                if (result is null)
                    throw new Exception("Nenhum planeta foi localizado.");

                var planetResult = service.GetPlanetByName(result.Name).Result;

                if (planetResult is not null)
                    count = planetResult.films.Count();

                var response = new PlanetResponse(result);
                response.Occurrences = count;

                var planet = new BaseResponse()
                {
                    StatusOk = true,
                    Planets = new List<PlanetResponse>()
                    {
                        response
                    }
                };

                return planet;
            }
            catch(Exception ex)
            {
                return new BaseResponse()
                {
                    Message = ex.Message
                };
            }

            
        }

        public BaseResponse ListPlanets()
        {
            var result = new List<PlanetResponse>();
            
            try
            {
                var planets = repository.ListPlanets();

                if (planets is null || planets.Count() == 0)
                    throw new Exception("Nenhum planeta foi encontrado");

                var planetList = service.GetPlanetList().Result;

                if (planetList.Count() == 0)
                    throw new Exception("Erro ao se conectar com a API externa.");

                foreach (var planet in planets)
                {
                    var response = new PlanetResponse(planet);

                    var planetData = planetList.Find(x => x.name.ToLower() == planet.Name.ToLower());
                    response.Occurrences = planetData != null ? planetData.films.Count() : 0;

                    result.Add(response);
                }

                return new BaseResponse()
                {
                    StatusOk = true,
                    Planets = result
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse()
                {
                    Message = ex.Message
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
