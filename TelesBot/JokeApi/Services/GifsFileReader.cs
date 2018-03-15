using JokeApi.Interfaces;
using JokeApi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi.Services
{
    public class GifsFileReader : IGifsFileReader
    {
        private readonly FileReader fileReader;

        public GifsFileReader()
        {
            fileReader = new FileReader("laugh_gifs.txt");
        }

        public async Task<IEnumerable<GifLinks>> GetGifLinksAsync()
        {
            var content = await fileReader.GetContent();
            var result = new List<GifLinks>();

            content.ToList().ForEach(fileLine => result.Add(CreateLaughLink(fileLine)));
            return result;
        }

        private GifLinks CreateLaughLink(string lineFromFile)
        {
            var elements = lineFromFile.Split("||");
            return new GifLinks(elements[0].Trim(), elements[1].Trim());
        }
    }
}
