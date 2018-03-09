using JokeApi.Helpers;
using JokeApi.Model;

namespace JokeApi.Interfaces
{
    public interface IJokeGenerator
    {
        Joke FindByCategory(JokeCategory jokeCategory);
    }
}
