using TelesBot.Helpers;

namespace TelesBot.Model
{
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public JokeCategory Category { get; set; }
        public string CategoryDescription { get; set; }

        public bool Exists() => !string.IsNullOrEmpty(Introduction);
    }
}