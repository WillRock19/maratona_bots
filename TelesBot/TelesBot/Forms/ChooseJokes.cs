using Microsoft.Bot.Builder.FormFlow;
using System;
using TelesBot.Extensions;
using TelesBot.Enums;

namespace TelesBot.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, \"**{0}**\" não é aceitável. Por favor, escolha uma opção válida (◑‿◐).")]
    public class ChooseJokes
    {
        [Prompt("Qual {&} você quer ouvir? {||}")]
        [Describe("tipo de piada")]
        public JokeCategory JokeType { get; set; }

        public static IForm<ChooseJokes> BuildForm()
        {
            var form = new FormBuilder<ChooseJokes>();

            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "yes", "s", "y", "yep", "é", "ye", "eh", "claro", "yeap", "quero", "quero sim", "isso", "é isso aí" };
            form.Configuration.No = new string[] { "não", "nao", "no", "not", "n", "nem", "nop", "not", "errado", "mudei de ideia", "nem" };
            form.Message("Você vai ter que me ajudar (∪ ◡ ∪)")
                .Field(nameof(JokeType))
                .Confirm(async (state) =>
                {
                    var joke = state.JokeType.GetDescribe();
                    var jokeMessage = joke.Equals(JokeCategory.DadJokes) || joke.Equals(JokeCategory.SuperHeroes) ? $"piada de **'{joke}'**" : $"piada **'{joke}'**";
                    return new PromptAttribute($"Então, você quer ouvir uma {jokeMessage}? (◔,◔)");
                });

            return form.Build();
        }
    }
}