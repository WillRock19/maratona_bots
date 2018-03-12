using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Threading;
using TelesBot.Dialogs;
using TelesBot.Extensions;
using TelesBot.Helpers;

namespace TelesBot.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, \"**{0}**\" não é um tipo aceitável. Por favor, escolha uma das opções válidas (◑‿◐).")]
    public class ChooseJokes
    {
        [Describe("tipo de piada")]
        public JokeCategory JokeType { get; set; }

        public static IForm<ChooseJokes> BuildForm()
        {
            var form = new FormBuilder<ChooseJokes>();

            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "yes", "s", "y", "yep", "é", "ye", "eh" };
            form.Configuration.No  = new string[] { "não", "nao", "no", "not", "n", "nem" };
            form.Message("Beleza, agora você vai ter que me ajudar (∪ ◡ ∪)");

            form.OnCompletion(async (context, sugestao) =>
            {
                var message = context.MakeMessage();
                message.Text = sugestao.JokeType.GetDescribe();

                await context.Forward(new JokeDialog(), null, message, CancellationToken.None);
            });

            return form.Build();
        }
    }
}