using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShorteningService.Services.Interfaces
{
    public interface IFileUpdaterService
    {
        void CreateShortUrl(string token, string url);
    }
}