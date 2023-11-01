using Moq;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Request;
using Xunit;

namespace Tests.Usecase
{
    public class AddPlanetTest
    {
        public AddPlanetTest() 
        {
            repository = new Mock<IPlanetRepository>();
            service = new Mock<IPlanetService>();
        }

        readonly Mock<IPlanetRepository> repository;
        readonly Mock<IPlanetService> service;

        readonly PlanetRequest request = new PlanetRequest()
        {
            Name = "name",
            Climate = "climate",
            Terrain = "terrain"
        };

        [Fact]
        public void AddPlanetShouldReturnSuccess()
        {
            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.AddPlanet(request);

            repository.Verify(x => x.AddPlanet(It.IsAny<PlanetRequest>()), Times.Once);
            Assert.True(result.StatusOk);
        }

        [Fact]
        public void AddPlanetShouldReturnErrorInRepository()
        {
            repository.Setup(x=>x.AddPlanet(It.IsAny<PlanetRequest>()))
                .Throws(new Exception());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.AddPlanet(request);

            repository.Verify(x => x.AddPlanet(It.IsAny<PlanetRequest>()), Times.Once);
            Assert.False(result.StatusOk);
        }
    }
}
