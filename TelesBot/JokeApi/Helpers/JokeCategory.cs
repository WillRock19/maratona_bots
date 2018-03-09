using System.ComponentModel;

namespace JokeApi.Helpers
{
    public enum JokeCategory
    {
        [Description("Humor Negro")]
        BlackHumor,

        [Description("Piadas de Tiozão")]
        DadJokes,

        [Description("Piadas de Super-Heróis")]
        SuperHeroes
    }
}
