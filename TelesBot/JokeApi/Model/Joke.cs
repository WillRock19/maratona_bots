using JokeApi.Helpers;

namespace JokeApi.Model
{
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public JokeCategory Category { get; set; }
    }
}
