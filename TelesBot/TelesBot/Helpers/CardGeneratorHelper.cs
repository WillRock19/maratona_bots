using Microsoft.Bot.Connector;
using System.Collections.Generic;
using TelesBot.Interfaces;

namespace TelesBot.Helpers
{
    public class CardGeneratorHelper : ICardGeneratorHelper
    {
        public CardGeneratorHelper() { }

        public Attachment SimpleAnimationCard(string title, string imageUrl,  string subtitle = "")
        {
            var heroCard = new AnimationCard()
            {
                Title = title,
                Subtitle = subtitle,
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = imageUrl
                    }
                },
            };

            return heroCard.ToAttachment();
        } 
    }
}