using Microsoft.Bot.Builder.FormFlow;

namespace TelesBot.Helpers
{
    public enum JokeCategory
    {
        [Describe("Especial do Teles-Bot >.>")]
        Especial = 1,

        [Describe("Nerd")]
        Nerd,

        [Describe("Tiozão")]
        DadJokes,

        [Describe("Super-Heróis")]
        SuperHeroes,
    }
}