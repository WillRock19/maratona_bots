using JokeApi.Interfaces;
using JokeApi.Model;
using System.Collections.Generic;
using System.IO;

namespace JokeApi.Services
{
    public class JokeFileReader : IJokeFileReader
    {
        private readonly string _filePath;
        private readonly string _fileName;

        public JokeFileReader()
        {
            _fileName = "jokes.txt";
            _filePath = $"{Directory.GetCurrentDirectory()}\\Files\\{_fileName}";
        }

        public IEnumerable<Joke> GetJokes()
        {
            var file = new StreamReader(_filePath);
            var line = string.Empty;
            var result = new List<Joke>();

            while ((line = file.ReadLine()) != null)
            {
                result.Add(CreateJoke(line));
            }

            file.Close();
            return result;
        }

        private Joke CreateJoke(string lineFromFile)
        {
            var jokeElements = lineFromFile.Split("||");
            return new Joke(jokeElements[0].Trim(), jokeElements[1].Trim(), jokeElements[2].Trim());
        }
    }
}
