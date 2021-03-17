using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using URLShorteningService.Model;
using URLShorteningService.Services.Interfaces;
using URLShorteningService.Utils;

namespace URLShorteningService.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly Dictionary<string, string> _tokenUrlDictionary;
        private readonly IJsonWrapper _jsonWrapper;

        public FileReaderService(IConfiguration config, IJsonWrapper jsonWrapper)
        {
            _tokenUrlDictionary = config.GetSection(Constants.UrlFilePath).GetChildren().ToDictionary(x => x.Key, x => x.Value);
            _jsonWrapper = jsonWrapper;
        }

        public (bool exists, string url) CheckShortUrlExistsInFile(string longUrl)
        {
            List<UrlModel> urlList = GetUrlItems();
            UrlModel res = null;

            if (urlList != null)
            {
                res = urlList.FirstOrDefault(x => x.Url.ToString() == longUrl);
            }

            return res != null ? (true, res.Token) : (false, "No URL found.");
        }

        public (bool exists, string url) CheckTokenExistsInFile(string token)
        {
            List<UrlModel> urlList = GetUrlItems();

            UrlModel res = urlList.FirstOrDefault(x => x.Token == token);

            return res != null ? (true, res.Url) : (false, "No Token found.");
        }

        public List<UrlModel> GetListUrls()
        {
            return GetUrlItems();
        }

        private List<UrlModel> GetUrlItems()
        {
            string dataFilePath = _tokenUrlDictionary[Constants.DataFileName];

            using StreamReader r = new StreamReader(dataFilePath);

            return _jsonWrapper.JsonDeserializeObject<List<UrlModel>>(r.ReadToEnd());
        }
    }
}