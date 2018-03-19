using System;
using TelesBot.Enums;

namespace TelesBot.Model
{
    [Serializable]
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public JokeCategory Category { get; set; }
        public string CategoryDescription { get; set; }
        public string GifUrl { get; set; }

        public bool Exists() => !string.IsNullOrEmpty(Introduction);
    }
}