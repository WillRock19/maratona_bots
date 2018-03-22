using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;
using System.Threading.Tasks;

namespace JokeApi.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private IJokeAdministrator _jokeAdministrator;

        public JokeRepository(IJokeAdministrator jokeAdministrator)
        {
            _jokeAdministrator = jokeAdministrator;
        }

        public async Task<Joke> FindByCategoryAsync(JokeCategory jokeCategory) =>
            await _jokeAdministrator.FindRandomJokeByCategory(jokeCategory);

        public async Task<Joke> FindHeroJokeByHeroNameAsync(string heroName) =>
            await _jokeAdministrator.FindJokeByHeroName(heroName);
    }
}
