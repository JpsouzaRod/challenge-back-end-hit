﻿namespace StarWarsChallenge.Domain.Core.Models.Adapter.Postgres
{
    public class PlanetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Terrain { get; set; }
        public string Climate { get; set; }
    }
}
