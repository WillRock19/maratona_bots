﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TelesBot.Enums;
using TelesBot.Extensions;
using TelesBot.Helpers;
using TelesBot.Services;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        private JokeSearcher jokeSearcher;
        private ImageRecognitionHandler imageRecognition;

        private CardGeneratorHelper cardGenerator;
        private Model.Joke jokeFinded;

        public JokeDialog()
        {
            jokeSearcher = new JokeSearcher();
            cardGenerator = new CardGeneratorHelper();
            imageRecognition = new ImageRecognitionHandler();
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
            PromptDialog.Attachment(
                context: context,
                resume: ResumeWithUserAttachment,
                prompt: "Me manda o simbolo de algum herói e vou ver se sei alguma piada sobre ele ^^",
                retry: "Não consegui entender... tenta manda uma imagem direto (não o link, a imagem mesmo) =D"
            );
        }

        private async Task ResumeWithUserAttachment(IDialogContext context, IAwaitable<IEnumerable<Attachment>> result)
        {
            var attachment = await result;
            var tag = await imageRecognition.getImageTags(attachment.FirstOrDefault());

            if (!string.IsNullOrEmpty(tag))
            {
                await context.PostAsync("Poxa... esse eu não conheço ಥ﹏ಥ");
            }
            else
            {

            }
        }

        private async Task GetSimpleJokeAndMakePun(IDialogContext context, JokeCategory category)
        {
            var result = jokeSearcher.GetJokeByCategory(category);
            var istyping = SendIsTyping(context, result);

            await Task.WhenAll(new[] { result, istyping });
            jokeFinded = result.Result;

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

                if (userLikedJoke) await context.PostAsync("Fico feliz em saber ლ(́◉◞౪◟◉‵ლ)");
                else await context.PostAsync("Ah... você que se foda então (｡͡•‿•｡)");
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync("Já que você não quer responder direito... se fode aí (╹◡╹)凸");
            }
            finally {
                FinalizeContextWithDone(context);
            }
        }
    }
}