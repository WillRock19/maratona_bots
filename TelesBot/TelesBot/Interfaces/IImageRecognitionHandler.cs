using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace TelesBot.Interfaces
{
    public interface IImageRecognitionHandler
    {
        Task<string> getImageTags(Attachment attachment);
    }
}
