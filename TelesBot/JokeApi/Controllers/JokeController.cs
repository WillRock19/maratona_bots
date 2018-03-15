using JokeApi.Helpers;
using JokeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<JsonResult> Get(JokeCategory? category)
        {
            try
            {
                if (category.HasValue)
                {
                    var joke = await _jokeRepository.FindByCategoryAsync(category.Value);
                    return new JsonResult(joke);
                    //return JsonConvert.SerializeObject(joke, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii }); Pode ser usado também
                }
                else
                    throw new ArgumentNullException("Category", "Nenhuma categoria válida informada!");
            }
            catch (Exception e) {
                return new JsonResult("Uma exceção ocorreu..." + e.Message);
            }
        }
    }
}
