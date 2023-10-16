namespace StarWarsChallenge.Domain.Core.Models
{
    public class PlanetResponse
    {
        public PlanetResponse(PlanetDTO planet)
        {
            Id = planet.Id;
            Name = planet.Name;
            Terrain = planet.Terrain;
            Climate = planet.Climate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Terrain { get; set; }
        public string Climate { get; set; }
        public int Occurrences { get; set; }
    }
}
