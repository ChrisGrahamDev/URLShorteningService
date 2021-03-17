using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShorteningService.DataAccess.Interfaces;

namespace URLShorteningService.Controllers
{
    public class UrlServiceController : Controller
    {
        private readonly IDataAccess _dataAccess;

        public UrlServiceController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpPost]
        [Route("GetUrl")]
        public IActionResult ShortenUrl([FromBody] string longUrl)
        {
            if (!string.IsNullOrEmpty(longUrl))
            {
                if (ValidateUrl(longUrl))
                {
                    string shortUrl = _dataAccess.GetShortUrl(longUrl);

                    // concat full url here
                    return Ok(string.Concat($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/{shortUrl}"));
                }
            }

            return BadRequest("Please include a valid URL.");
        }

        [HttpPost]
        [EnableCors]
        [Route("/{token}")]
        public IActionResult RedirectUrl(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                (bool exists, string shortUrl) result = _dataAccess.CheckTokenExists(token);

                if (result.exists)
                {
                    return RedirectPreserveMethod(result.shortUrl);
                }
            }

            return BadRequest("Short Url not found. Please check short url and try again.");
        }

        private bool ValidateUrl(string longUrl)
        {
            return Uri.TryCreate(longUrl, UriKind.Absolute, out _);
        }
    }
}