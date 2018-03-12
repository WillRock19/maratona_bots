using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JokeApi.Services
{
    public class JokeAdministrator : IJokeAdministrator
    {
        private readonly IEnumerable<Joke> _jokes;
        private readonly IJokeFileReader _fileReader;

        public JokeAdministrator(IJokeFileReader fileReader)
        {
            _jokes = new List<Joke>();
            _fileReader = fileReader;
            _jokes = LoadJokeList();
        }

        public Joke FindRandomJokeByCategory(JokeCategory jokeCategory)
        {
            if(!JokeListExists())
                LoadJokeList();

            var findedJokes = _jokes.Where(j => j.Category.Equals(jokeCategory))
                                   .ToList();

            return findedJokes.ElementAt(RandomJokePosition(findedJokes.Count()));
        }

        private bool JokeListExists() => _jokes.Any();

        private IEnumerable<Joke> LoadJokeList() => _fileReader.GetJokes();

        private int RandomJokePosition(int numberOfJokes) => new Random().Next(0, numberOfJokes);
    }
}
