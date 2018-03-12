using System.ComponentModel;

namespace JokeApi.Helpers
{
    public enum JokeCategory
    {
        [Description("Tiozão")]
        DadJokes,

        [Description("Super-Heróis")]
        SuperHeroes,

        [Description("Nerd")]
        Nerd,

        [Description("Especial")]
        Especial,

        Undefined
    }
}
