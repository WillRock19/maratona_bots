using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;

namespace TelesBot.Helpers
{
    [Serializable]
    public class CardGeneratorHelper
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