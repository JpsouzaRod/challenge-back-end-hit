using Moq;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Adapter.Postgres;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;

namespace Tests.Usecase
{
    public class ListPlanetTest
    {
        public ListPlanetTest()
        {
            repository = new Mock<IPlanetRepository>();
            service = new Mock<IPlanetService>();
        }

        List<PlanetDTO> list = new List<PlanetDTO>()
        {
            new PlanetDTO()
            {
                Id = 1,
                Name = "Tatooine",
                Climate = "Test",
                Terrain = "Test"
            },
            new PlanetDTO()
            {
                Id = 2,
                Name = "Teste",
                Climate = "Test",
                Terrain = "Test"
            } 
        };

        Planet planetService = new Planet()
        {
            name = "Tatooine",
            films = new List<string>()
            {
                "teste",
                "teste2"
            }
        };
        readonly Mock<IPlanetRepository> repository;
        readonly Mock<IPlanetService> service;

        [Fact]
        public void ListPlanetByIdShouldReturnSuccess()
        {
            repository.Setup(x => x.ListPlanets())
                .Returns(list);

            service.Setup(x => x.GetPlanetList())
                .ReturnsAsync(new List<Planet>() { planetService });

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.ListPlanets();

            repository.Verify(x => x.ListPlanets(),Times.Once);
            service.Verify(x => x.GetPlanetList(), Times.Once);

            Assert.True(result.StatusOk);
            Assert.Equal(list.Count(), result.Planets.Count());
        }

        [Fact]
        public void ListPlanetByIdShouldReturnErrorInRepository()
        {
            repository.Setup(x => x.ListPlanets())
                .Returns(new List<PlanetDTO>());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.ListPlanets();

            repository.Verify(x => x.ListPlanets(), Times.Once);

            Assert.False(result.StatusOk);
        }

        [Fact]
        public void FindPlanetByNameShouldReturnException()
        {
            repository.Setup(x => x.ListPlanets())
                .Throws(new Exception());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.ListPlanets();

            repository.Verify(x => x.ListPlanets(), Times.Once);

            Assert.False(result.StatusOk);
        }
    }
}
