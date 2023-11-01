using Moq;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Adapter.Postgres;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;
using StarWarsChallenge.Domain.Core.Models.Request;

namespace Tests.Usecase
{
    public class FindPlanetByIdTest
    {
        public FindPlanetByIdTest()
        {
            repository = new Mock<IPlanetRepository>();
            service = new Mock<IPlanetService>();
        }

        readonly Mock<IPlanetRepository> repository;
        readonly Mock<IPlanetService> service;

        int request = 1;

        PlanetDTO planetDTO = new PlanetDTO()
        {
            Id = 1,
            Name = "Test",
            Climate = "Climate",
            Terrain = "Terrain"
        };

        Planet planet = new Planet()
        {
            name = "Test",
            climate = "Climate",
            terrain = "Terrain",
            films = new List<string>()
            {
                "Test",
                "Test2"
            }
        };

        [Fact]
        public void FindPlanetByIdShouldReturnSuccess()
        {
            repository.Setup(x => x.FindPlanetById(It.IsAny<int>()))
                .Returns(planetDTO);

            service.Setup(x => x.GetPlanetByName(It.IsAny<string>()))
                .ReturnsAsync(planet);

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetById(request);

            repository.Verify(x => x.FindPlanetById(It.IsAny<int>()), Times.Once);
            service.Verify(x => x.GetPlanetByName(It.IsAny<string>()), Times.Once);

            Assert.True(result.StatusOk);
            Assert.Equal(2, result.Planets.First().Occurrences);
        }

        [Fact]
        public void FindPlanetByIdShouldReturnErrorInRepository()
        {
            repository.Setup(x => x.FindPlanetById(It.IsAny<int>()))
                .Returns(value: null);

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetById(request);

            repository.Verify(x => x.FindPlanetById(It.IsAny<int>()), Times.Once);

            Assert.False(result.StatusOk);
        }

        [Fact]
        public void FindPlanetByIdShouldReturnException()
        {
            repository.Setup(x => x.FindPlanetById(It.IsAny<int>()))
                .Throws(new Exception());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetById(request);

            repository.Verify(x => x.FindPlanetById(It.IsAny<int>()), Times.Once);

            Assert.False(result.StatusOk);
        }
    }
}
