using Moq;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Adapter.Postgres;
using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;
using StarWarsChallenge.Domain.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Usecase
{
    public class FindPlanetByNameTest
    {
        public FindPlanetByNameTest() {
            repository = new Mock<IPlanetRepository>();
            service = new Mock<IPlanetService>();
        }

        readonly Mock<IPlanetRepository> repository;
        readonly Mock<IPlanetService> service;

        string request = "Tatooine";

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
        public void FindPlanetByNameShouldReturnSuccess()
        {
            repository.Setup(x => x.FindPlanetByName(It.IsAny<string>()))
                .Returns(planetDTO);

            service.Setup(x => x.GetPlanetByName(It.IsAny<string>()))
                .ReturnsAsync(planet);

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetByName(request);

            repository.Verify(x => x.FindPlanetByName(It.IsAny<string>()), Times.Once);
            service.Verify(x => x.GetPlanetByName(It.IsAny<string>()), Times.Once);

            Assert.True(result.StatusOk);
            Assert.Equal(2, result.Planets.First().Occurrences);
        }

        [Fact]
        public void FindPlanetByNameShouldReturnErrorInRepository()
        {
            repository.Setup(x => x.FindPlanetByName(It.IsAny<string>()))
                .Returns(value: null);

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetByName(request);

            repository.Verify(x => x.FindPlanetByName(It.IsAny<string>()), Times.Once);

            Assert.False(result.StatusOk);
        }

        [Fact]
        public void FindPlanetByNameShouldReturnException()
        {
            repository.Setup(x => x.FindPlanetByName(It.IsAny<string>()))
                .Throws(new Exception());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.FindPlanetByName(request);

            repository.Verify(x => x.FindPlanetByName(It.IsAny<string>()), Times.Once);

            Assert.False(result.StatusOk);
        }
    }
}
