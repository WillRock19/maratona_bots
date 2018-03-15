using Microsoft.Bot.Builder.FormFlow;

namespace TelesBot.Enums
{
    public enum WishForAnotherJoke
    {
        [Describe("Sim!")]
        Yes,

        [Describe("Não!")]
        No
    }
}