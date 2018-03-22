using JokeApi.Helpers;
using JokeApi.Model;
using System.Threading.Tasks;

namespace JokeApi.Interfaces
{
    public interface IJokeRepository
    {
        Task<Joke> FindByCategoryAsync(JokeCategory jokeCategory);
        Task<Joke> FindHeroJokeByHeroNameAsync(string heroName);
    }
}
