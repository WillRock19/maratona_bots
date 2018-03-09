using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;

namespace JokeApi.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private IJokeGenerator jokeGenerator;

        public JokeRepository(IJokeGenerator jokeGenerator)
        {
            this.jokeGenerator = jokeGenerator;
        }

        public Joke FindByCategory(JokeCategory jokeCategory) =>
            jokeGenerator.FindByCategory(jokeCategory);
    }
}
