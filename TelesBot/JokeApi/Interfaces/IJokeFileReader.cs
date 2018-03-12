using JokeApi.Model;
using System.Collections.Generic;

namespace JokeApi.Interfaces
{
    public interface IJokeFileReader
    {
        IEnumerable<Joke> GetJokes();
    }
}
