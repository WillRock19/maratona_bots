using JokeApi.Extensions;
using JokeApi.Helpers;
using JokeApi.Interfaces;
using JokeApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers
{
    [Route("api/[controller]")]
    public class JokeController : Controller
    {
        private readonly IJokeRepository _jokeRepository;
        public JokeController(IJokeRepository jokeRepository)
        {
            _jokeRepository = jokeRepository;
        }

        [HttpGet]
        public Joke ByRandomCategory(string category)
        {
            return _jokeRepository.FindByCategory(category.ToEnumThatHasThisDescription<JokeCategory>());
        }
    }
}
