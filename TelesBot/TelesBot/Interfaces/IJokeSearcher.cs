using System.Threading.Tasks;
using TelesBot.Enums;
using TelesBot.Model;

namespace TelesBot.Interfaces
{
    public interface IJokeSearcher
    {
        Task<Joke> GetJokeByCategory(JokeCategory category);
        Task<Joke> GetJokeBySuperHero(string heroName);
    }
}
