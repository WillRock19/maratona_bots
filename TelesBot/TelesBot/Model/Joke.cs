using System;

namespace TelesBot.Model
{
    [Serializable]
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public string CategoryDescription { get; set; }
        public string GifUrl { get; set; }

        public bool Exists() => !string.IsNullOrEmpty(Introduction);
    }
}