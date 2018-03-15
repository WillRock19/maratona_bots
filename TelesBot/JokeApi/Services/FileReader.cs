using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JokeApi.Services
{
    public class FileReader
    {
        private readonly string _filePath;
        private readonly string _fileName;

        public FileReader(string fileName)
        {
            _fileName = fileName;
            _filePath = $"{Directory.GetCurrentDirectory()}\\Files\\{_fileName}";
        }

        public async Task<IReadOnlyCollection<string>> GetContent() => await File.ReadAllLinesAsync(_filePath, Encoding.UTF8);
    }
}
