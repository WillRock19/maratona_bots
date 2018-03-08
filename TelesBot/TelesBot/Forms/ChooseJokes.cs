using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using TelesBot.Helpers;

namespace TelesBot.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, \"**{0}**\" não é um tipo aceitável. Por favor, escolha uma das opções válidas (◑‿◐).")]
    public class ChooseJokes
    {
        [Describe("tipo de piada")]
        public JokeType JokeType { get; set; }

        public static IForm<ChooseJokes> BuildForm()
        {
            var form = new FormBuilder<ChooseJokes>();

            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "yes", "s", "y", "yep", "é", "ye" };
            form.Configuration.No  = new string[] { "não", "nao", "no", "not", "n", "nem" };
            form.Message("Beleza, agora você vai ter que me ajudar (∪ ◡ ∪)");

            form.OnCompletion(async (context, sugestao) =>
            {
                await context.PostAsync("Ok, vamos procurar algo divertido...");
            });

            return form.Build();
        }
    }
}