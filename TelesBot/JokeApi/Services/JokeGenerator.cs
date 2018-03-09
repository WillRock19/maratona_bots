using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JokeApi.Services
{
    public class JokeGenerator : IJokeGenerator
    {
        private readonly IList<Joke> jokes;

        public JokeGenerator()
        {
            jokes = new List<Joke>();
            LoadJokeList();
        }

        public Joke FindByCategory(JokeCategory jokeCategory)
        {
            if(!JokeListExists())
                LoadJokeList();

            var findedJokes = jokes.Where(j => j.Category.Equals(jokeCategory))
                                   .ToList();

            return findedJokes.ElementAt(RandomJokePosition(findedJokes.Count()));
        }

        private bool JokeListExists() => jokes.Any();

        private void LoadJokeList() { }

        private int RandomJokePosition(int numberOfJokes) => new Random().Next(0, numberOfJokes);
    }
}
