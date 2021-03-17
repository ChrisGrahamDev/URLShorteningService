using System.Collections.Generic;
using URLShorteningService.Model;

namespace URLShorteningService.Services.Interfaces
{
    public interface IFileReaderService
    {
        (bool exists, string url) CheckShortUrlExistsInFile(string url);

        (bool exists, string url) CheckTokenExistsInFile(string token);

        List<UrlModel> GetListUrls();
    }
}