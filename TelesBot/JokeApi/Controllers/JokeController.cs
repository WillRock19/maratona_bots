using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JokeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JokeApi.Controllers
{
    [Route("api/[controller]")]
    public class JokeController : Controller
    {
        private readonly IJokeRepository jokeRepository;

        public JokeController(IJokeRepository jokeRepository)
        {
            this.jokeRepository = jokeRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
