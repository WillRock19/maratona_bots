using System.ComponentModel;

namespace JokeApi.Helpers
{
    public enum JokeCategory
    {
        [Description("Nerd")]
        Nerd,

        [Description("Tiozão")]
        DadJokes,

        [Description("Especial")]
        Special,

        [Description("Super-Heróis")]
        SuperHeroes,

        [Description("Piada sem categoria")]
        Undefined
    }
}
