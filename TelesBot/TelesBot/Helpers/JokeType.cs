using Microsoft.Bot.Builder.FormFlow;

namespace TelesBot.Helpers
{
    public enum JokeType
    {
        [Describe("Humor Negro")]
        BlackHumor = 1, //Have to start with 1 so formFlow can understand it

        [Describe("Piadas de Tiozão")]
        DadJokes,

        [Describe("Piadas de Super-Heróis")]
        SuperHeroes
    }
}