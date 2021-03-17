using System;
using URLShorteningService.DataAccess.Interfaces;

namespace URLShorteningService.DataAccess
{
    public class DatabaseDataAccess : IDataAccess
    {
        public string GetShortUrl(string longUrl)
        {
            throw new NotImplementedException();
        }

        public (bool exists, string shortUrl) CheckTokenExists(string token)
        {
            throw new NotImplementedException();
        }

        public string AddShortUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}