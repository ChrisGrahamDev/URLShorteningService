using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using URLShorteningService.Model;
using URLShorteningService.Services.Interfaces;
using URLShorteningService.Utils;

namespace URLShorteningService.Services
{
    public class FileUpdaterService : IFileUpdaterService
    {
        private readonly IFileReaderService _fileReaderService;
        private readonly IJsonWrapper _jsonWrapper;
        private readonly Dictionary<string, string> _filePathConfig;

        public FileUpdaterService(IFileReaderService fileReaderService, IJsonWrapper jsonWrapper, IConfiguration config)
        {
            _fileReaderService = fileReaderService;
            _jsonWrapper = jsonWrapper;
            _filePathConfig = config.GetSection(Constants.UrlFilePath).GetChildren().ToDictionary(x => x.Key, x => x.Value);
        }

        public void CreateShortUrl(string token, string url)
        {
            List<UrlModel> listItems = _fileReaderService.GetListUrls();

            var newItem = new UrlModel
            {
                Token = token,
                Url = url
            };

            listItems ??= new List<UrlModel>();
            
            listItems.Add(newItem);

            WriteTextToFile(_filePathConfig[Constants.DataFileName], _jsonWrapper.JsonSerializeObject(listItems));
        }

        private void WriteTextToFile(string filepath, string fileContent)
        {
            File.WriteAllText(filepath, fileContent);
        }
    }
}