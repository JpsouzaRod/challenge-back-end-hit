using StarWarsChallenge.Domain.Core.Models.Adapter.Swapi;

namespace StarWarsChallenge.Adapter.StarWarsApi.Models
{
    public class ApiResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Planet> results { get; set; }
    }
}
