using JokeApi.Helpers;
using JokeApi.Model;

namespace JokeApi.Interfaces
{
    public interface IJokeAdministrator
    {
        Joke FindRandomJokeByCategory(JokeCategory jokeCategory);
    }
}
