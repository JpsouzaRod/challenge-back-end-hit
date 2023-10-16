using Dapper;
using StarWarsChallenge.Adapter.Postgres.Context;
using StarWarsChallenge.Domain.Application.Interface;
using StarWarsChallenge.Domain.Core.Models;

namespace StarWarsChallenge.Adapter.Postgres.Repository
{
    public class PlanetRepository : IPlanetRepository
    {
        public PlanetRepository(DbContext _context) 
        { 
            context = _context;
        }

        private readonly DbContext context;
        public void AddPlanet(PlanetRequest planet)
        {
            using (var con = context.CreateConnection())
            {
                const string sql = "INSERT INTO planet (name, terrain, climate) VALUES (@name, @terrain, @climate)";
                con.Execute(sql, new { name = planet.Name, terrain = planet.Terrain, climate = planet.Climate });
                    
            }
        }

        public PlanetDTO FindPlanetById(int id)
        {
            using (var con = context.CreateConnection())
            {
                const string sql = "SELECT * FROM planet WHERE id = @id";
                var result = con.QueryFirstOrDefault<PlanetDTO>(sql, new {id = id});

                return result;
            }
        }

        public PlanetDTO FindPlanetByName(string name)
        {
            using (var con = context.CreateConnection())
            {
                const string sql = "SELECT * FROM planet WHERE name = @name";
                var result = con.QueryFirstOrDefault<PlanetDTO>(sql, new { name = name });

                return result;
            }
        }

        public IList<PlanetDTO> ListPlanets()
        {
            using (var con = context.CreateConnection())
            {
                const string sql = "SELECT * FROM planet";
                var result = con.Query<PlanetDTO>(sql).ToList();

                return result;
            }
        }

        public void RemovePlanet(int id)
        {
            using (var con = context.CreateConnection())
            {
                const string sql = "DELETE FROM planet WHERE id=@id";
                con.Execute(sql, new { id = id });
            }
        }
    }
}
