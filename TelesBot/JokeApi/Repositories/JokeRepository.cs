using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;

namespace JokeApi.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private IJokeAdministrator _jokeAdministrator;

        public JokeRepository(IJokeAdministrator jokeAdministrator)
        {
            _jokeAdministrator = jokeAdministrator;
        }

        public Joke FindByCategory(JokeCategory jokeCategory) =>
            _jokeAdministrator.FindRandomJokeByCategory(jokeCategory);
    }
}
