using JokesApp.Helpers;

namespace JokesApp.Models
{
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public JokeType JokeType { get; set; }
    }
}
