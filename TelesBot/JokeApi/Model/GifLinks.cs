using JokeApi.Extensions;
using JokeApi.Helpers;

namespace JokeApi.Model
{
    public class GifLinks
    {
        public string linkUrl { get; set; }
        public JokeCategory jokeCategory { get; set; }

        public GifLinks(string link, string category = "")
        {
            linkUrl = link;
            jokeCategory = !string.IsNullOrEmpty(category) ? category.ToEnumWWithThisDescription<JokeCategory>() : JokeCategory.Undefined;
        }
    }
}
