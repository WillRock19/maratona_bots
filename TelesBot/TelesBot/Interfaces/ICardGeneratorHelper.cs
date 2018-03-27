using Microsoft.Bot.Connector;

namespace TelesBot.Interfaces
{
    public interface ICardGeneratorHelper
    {
        Attachment SimpleAnimationCard(string title, string imageUrl, string subtitle = "");
    }
}
