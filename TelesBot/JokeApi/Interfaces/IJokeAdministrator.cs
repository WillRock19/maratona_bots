using JokeApi.Helpers;
using JokeApi.Model;
using System.Threading.Tasks;

namespace JokeApi.Interfaces
{
    public interface IJokeAdministrator
    {
        Task<Joke> FindRandomJokeByCategory(JokeCategory jokeCategory);
        Task<Joke> FindJokeByHeroName(string heroName);
    }
}
