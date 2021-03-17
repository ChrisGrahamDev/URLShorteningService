using URLShorteningService.DataAccess.Interfaces;
using URLShorteningService.Services.Interfaces;

namespace URLShorteningService.DataAccess
{
    public class FileSystemDataAccess : IDataAccess
    {
        private readonly IFileUpdaterService _fileUpdaterService;
        private readonly IFileReaderService _fileReaderService;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public FileSystemDataAccess(IFileUpdaterService fileUpdaterService, IFileReaderService fileReaderService, ITokenGeneratorService tokenGeneratorService)
        {
            _fileUpdaterService = fileUpdaterService;
            _fileReaderService = fileReaderService;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public string GetShortUrl(string longUrl)
        {
            (bool exists, string url) res = _fileReaderService.CheckShortUrlExistsInFile(longUrl);

            if (res.exists == false)
            {
                res.url = AddShortUrl(longUrl);
                res.exists = true;
            }

            return res.url;
        }

        public (bool exists, string shortUrl) CheckTokenExists(string token)
        {
            (bool exists, string url) res = _fileReaderService.CheckTokenExistsInFile(token);

            return res;
        }

        private string AddShortUrl(string url)
        {
            string token = _tokenGeneratorService.GenerateToken();

            _fileUpdaterService.CreateShortUrl(token, url);

            return token;
        }
    }
}