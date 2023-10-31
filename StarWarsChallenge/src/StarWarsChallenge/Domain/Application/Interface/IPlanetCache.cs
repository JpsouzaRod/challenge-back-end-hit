namespace StarWarsChallenge.Domain.Application.Interface
{
    public interface IPlanetCache
    {
        Task SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
    }
}
