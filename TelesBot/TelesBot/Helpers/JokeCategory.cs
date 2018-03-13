using Microsoft.Bot.Builder.FormFlow;

namespace TelesBot.Helpers
{
    public enum JokeCategory
    {
        [Terms("inteligente", "nerd", "inteligentão", "esperta", "bem nerd")]
        [Describe("Nerd")]
        Nerd = 1,

        [Terms("Especial do tio Bot :D", "tio bot", "especial", ":D", "do tio bot", "do bot", "especial bot", "especial :D", "especial do tio")]
        [Describe("Especial do tio Bot :D")]
        Especial,

        [Terms("tiozão", "tiozao", "tiosao", "de tio", "do tio", "tio")]
        [Describe("Tiozão")]
        DadJokes,

        [Terms("super_Heróis", "super_herois", "superheróis", "super-heróis", "super-heroi", "super-herói", "supers", "super", "heróis", "herois", "herói", "heroi")]
        [Describe("Super-Heróis")]
        SuperHeroes,
    }
}