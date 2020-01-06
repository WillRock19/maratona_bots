using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TelesBot.Enums;
using TelesBot.Extensions;
using TelesBot.Interfaces;
using TelesBot.Model;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        private IJokeSearcher jokeSearcher;
        private IImageRecognitionHandler imageRecognition;
        private ICardGeneratorHelper cardGenerator;
        private IPromptDialogGenerator promptGenerator;
        private Joke jokeFinded;

        public JokeDialog(IJokeSearcher jokeSearcher, IImageRecognitionHandler imageRecognition, ICardGeneratorHelper cardGenerator, IPromptDialogGenerator promptGenerator)
        {
            this.cardGenerator = cardGenerator;
            this.imageRecognition = imageRecognition;
            this.jokeSearcher = jokeSearcher;
            this.promptGenerator = promptGenerator;
        }

        [LuisIntent("ContarPiada.Simples")]
        public async Task ContarUmaPiadaSimples(IDialogContext context, LuisResult result)
        {
            var category = result.Query.ToEnumByDescribe<JokeCategory>();

            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");
            await GetSimpleJokeAndMakePun(context, category);
        }

        [LuisIntent("ContarPiada.Herois")]
        public async Task ContarUmaPiadaDeSuperHeroi(IDialogContext context, LuisResult result)
        {
            await AskForSuperheroSimbol(context, "Me manda o simbolo de algum herói e vou ver se sei alguma piada sobre ele ^^");
        }

        private async Task AskForSuperheroSimbol(IDialogContext context, string message)
        {
            PromptDialog.Attachment(
                context: context,
                resume: ResumeWithUserAttachment,
                prompt: message,
                retry: "Não consegui entender... tenta manda uma imagem direto (não o link, a imagem mesmo) =D"
            );
        }

        private async Task ResumeWithUserAttachment(IDialogContext context, IAwaitable<IEnumerable<Attachment>> result)
        {
            var attachment = await result;
            var tagSearch = imageRecognition.getImageTags(attachment.FirstOrDefault());
            await SendTypingMessageAndWaitOperation(context, tagSearch);

            var superHeroName = tagSearch.Result;

            if (string.IsNullOrEmpty(superHeroName))
            {
                await context.PostAsync("Poxa... esse eu não conheço ಥ﹏ಥ");
                promptGenerator.ConfirmDialog(context, TryAnotherSimbol, "Quer tentar com outra imagem?", attemptsAllowed: 2);
            }
            else
            {
                await context.PostAsync($"Beleza... então você quer uma piada sobre o **'{tagSearch.Result}'**, né? Peraí...");
                await GetHeroJokeAndMakePun(context, tagSearch.Result);
            }
        }

        private async Task TryAnotherSimbol(IDialogContext context, IAwaitable<bool> result)
        {
            try
            {
                var tryAnother = await result;

                if (tryAnother)
                    await AskForSuperheroSimbol(context, "Ok, manda outro e vou tentar de novo ^^");
                else
                    FinalizeContextWithFail(context, "Ok... me desculpa ಥ﹏ಥ");
            }
            catch(TooManyAttemptsException e)
            {
                FinalizeContextWithFail(context, "Ah, já que é assim, me chama quando quiser alguma coisa (︶︹︺)");
            }
        }


        private async Task GetHeroJokeAndMakePun(IDialogContext context, string HeroName)
        {
            var jokeSearch = jokeSearcher.GetJokeBySuperHero(HeroName);
            await SendTypingMessageAndWaitOperation(context, jokeSearch);

            jokeFinded = jokeSearch.Result;

            await MakeJoke(context);
        }

        private async Task GetSimpleJokeAndMakePun(IDialogContext context, JokeCategory category)
        {
            var jokeSearch = jokeSearcher.GetJokeByCategory(category);
            await SendTypingMessageAndWaitOperation(context, jokeSearch);

            jokeFinded = jokeSearch.Result;

            await MakeJoke(context);
        }

        private async Task MakeJoke(IDialogContext context)
        {
            if (jokeFinded.Exists())
            {
                await context.PostAsync(jokeFinded.Introduction);
                context.Wait(ListenUserResponseAndFinishJoke);
            }
            else FinalizeContextWithFail(context, "Desculpa... ಥ﹏ಥ... não consegui pensar numa piada...");
        }

        private async Task ListenUserResponseAndFinishJoke(IDialogContext context, IAwaitable<object> result)
        {
            var userResponse = await result;
            var message = context.MakeMessage();

            message.Text = $"**'{jokeFinded.Conclusion}'**"; 
            message.Attachments.Add(cardGenerator.SimpleAnimationCard("", jokeFinded.GifUrl));

            await context.PostAsync(message);
            await Task.Delay(3000).ContinueWith(async t =>
            {
                await AskUserIfLikedJoke(context);
            });
        }

        private async Task AskUserIfLikedJoke(IDialogContext context)
        {
            PromptDialog.Confirm(
                context: context,
                resume: AnswerUserThoughtsAboutJoke,
                prompt: "E aí... gostou? ( ・ω・)",
                retry: "Opção escolhida inválida. Favor, escolher uma das disponíveis.",
                promptStyle: PromptStyle.Auto
            );
        }

        private async Task AnswerUserThoughtsAboutJoke(IDialogContext context, IAwaitable<bool> result)
        {
            try{
                var userLikedJoke = await result;

                if (userLikedJoke)
                {
                    await context.PostAsync("Fico feliz em saber ლ(́◉◞౪◟◉‵ლ)");
                    context.Done("E ai... quer ouvir outra...? (♥‿♥)");
                } 
                else
                {
                    await context.PostAsync("Ah... que pena x.x"); //você que se foda então (｡•‿•｡)凸
                    context.Done("Deixa eu tentar contar outra? (◔ ◡ ◔)");
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync("Já que você não quer conversar direito... fica ai");
                FinalizeContextWithFail(context, "Quando quiser seriamente uma zoeira, me dá um toque. (︶︹︺)");
            }
        }
    }
}