namespace StarWarsChallenge.Domain.Core.Models
{
    public class BaseResponse
    { 
        public BaseResponse()
        {
            Planets = new List<PlanetResponse>();
        }
        public bool StatusOk { get; set; }
        public string Message { get; set; }
        public List<PlanetResponse> Planets { get; set; }
    }
}
