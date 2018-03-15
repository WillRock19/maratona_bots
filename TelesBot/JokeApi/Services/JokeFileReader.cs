using JokeApi.Interfaces;
using JokeApi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi.Services
{
    public class JokeFileReader : IJokeFileReader
    {
        private readonly FileReader fileReader;

        public JokeFileReader()
        {
            fileReader = new FileReader("jokes.txt");
        }

        public async Task<IEnumerable<Joke>> GetJokesAsync()
        {
            var content = await fileReader.GetContent();
            var result = new List<Joke>();

            content.ToList().ForEach(fileLine => result.Add(CreateJoke(fileLine)));
            return result;
        }

        private Joke CreateJoke(string lineFromFile)
        {
            var jokeElements = lineFromFile.Split("||");
            return new Joke(jokeElements[0].Trim(), jokeElements[1].Trim(), jokeElements[2].Trim());
        }
    }
}
