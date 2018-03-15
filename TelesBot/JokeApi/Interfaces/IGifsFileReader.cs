using JokeApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokeApi.Interfaces
{
    public interface IGifsFileReader
    {
        Task<IEnumerable<GifLinks>> GetGifLinksAsync();
    }
}
