using JokeApi.Extensions;
using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi.Services
{
    public class JokeAdministrator : IJokeAdministrator
    {
        private readonly IJokeFileReader _fileReader;
        private readonly IGifsFileReader _gifsReader;

        private IEnumerable<Joke> _jokes;
        private IEnumerable<GifLinks> _gifs;

        public JokeAdministrator(IJokeFileReader fileReader, IGifsFileReader gifsReader)
        {
            _jokes = new List<Joke>();
            _fileReader = fileReader;
            _gifsReader = gifsReader;
        }

        public async Task<Joke> FindRandomJokeByCategory(JokeCategory jokeCategory)
        {
            if(!JokeListExists())
                await LoadJokeListAsync();

            var findedJokes = _jokes.Where(j => j.Category.Equals(jokeCategory))
                                   .ToList();

            if(findedJokes.Any())
                return findedJokes.ElementAt(RandomElementPosition(findedJokes.Count()));

            return new Joke();
        }

        public async Task<Joke> FindJokeByHeroName(string heroName)
        {
            var description = $"Heroi_{heroName}";
            var category = description.ToEnumWWithThisDescription<JokeCategory>();

            return await FindRandomJokeByCategory(category);
        }

        private bool JokeListExists() => _jokes.Any();

        private async Task LoadJokeListAsync()
        {
            _gifs = await _gifsReader.GetGifLinksAsync();
            _jokes = await _fileReader.GetJokesAsync();

            AssociateRandomGifToJokes();
        }

        private int RandomElementPosition(int numberOfElements) => new Random().Next(0, numberOfElements);

        private void AssociateRandomGifToJokes()
        {
            var specialGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Special);
            var nerdGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Nerd);
            var dadGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.DadJokes);

            var BatmanGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Hero_Batman);
            var DaredevilGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Hero_Daredevil);
            var HulkGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Hero_Hulk);
            var XmenGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Hero_Xmen);
            var SupermanGifs = _gifs.Where(x => x.jokeCategory == JokeCategory.Hero_Superman);

            foreach (var joke in _jokes)
            {
                switch (joke.Category)
                {
                    case JokeCategory.DadJokes:
                        joke.GifUrl = dadGifs.ElementAt(RandomElementPosition(dadGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Special:
                        joke.GifUrl = specialGifs.ElementAt(RandomElementPosition(specialGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Nerd:
                        joke.GifUrl = nerdGifs.ElementAt(RandomElementPosition(nerdGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Hero_Batman:
                        joke.GifUrl = BatmanGifs.ElementAt(RandomElementPosition(BatmanGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Hero_Daredevil:
                        joke.GifUrl = DaredevilGifs.ElementAt(RandomElementPosition(DaredevilGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Hero_Hulk:
                        joke.GifUrl = HulkGifs.ElementAt(RandomElementPosition(HulkGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Hero_Xmen:
                        joke.GifUrl = XmenGifs.ElementAt(RandomElementPosition(XmenGifs.Count())).linkUrl;
                        break;

                    case JokeCategory.Hero_Superman:
                        joke.GifUrl = SupermanGifs.ElementAt(RandomElementPosition(SupermanGifs.Count())).linkUrl;
                        break;
                }
            }
        }
    }
}
