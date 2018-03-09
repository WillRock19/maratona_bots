using JokeApi.Helpers;
using JokeApi.Model;

namespace JokeApi.Interfaces
{
    public interface IJokeRepository
    {
        Joke FindByCategory(JokeCategory jokeCategory);
    }
}
