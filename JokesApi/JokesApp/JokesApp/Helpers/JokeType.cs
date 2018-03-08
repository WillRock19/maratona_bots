using System.ComponentModel;

namespace JokesApp.Helpers
{
    public enum JokeType
    {
        [Description("Humor Negro")]
        BlackHumor,
        [Description("Piadas de Tiozão")]
        DadJokes,
        [Description("Piadas de Super-Heróis")]
        SuperHeroes
    }
}
