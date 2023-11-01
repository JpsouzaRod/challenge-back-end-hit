using Moq;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Application.Usecase;
using StarWarsChallenge.Domain.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Usecase
{
    public class RemovePlanetByIdTest
    {
        public RemovePlanetByIdTest() {
            repository = new Mock<IPlanetRepository>();
            service = new Mock<IPlanetService>();
        }

        readonly Mock<IPlanetRepository> repository;
        readonly Mock<IPlanetService> service;

        int request = 1;

        [Fact]
        public void RemovePlanetShouldReturnSuccess()
        {
            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.RemovePlanetById(request);

            repository.Verify(x => x.RemovePlanet(It.IsAny<int>()), Times.Once);
            Assert.True(result.StatusOk);
        }

        [Fact]
        public void RemovePlanetShouldReturnErrorInRepository()
        {
            repository.Setup(x => x.RemovePlanet(It.IsAny<int>()))
                .Throws(new Exception());

            var usecase = new PlanetUsecase(repository.Object, service.Object);
            var result = usecase.RemovePlanetById(request);

            repository.Verify(x => x.RemovePlanet(It.IsAny<int>()), Times.Once);
            Assert.False(result.StatusOk);
        }
    }
}
