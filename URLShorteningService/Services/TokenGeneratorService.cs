using System;
using System.Linq;
using URLShorteningService.Services.Interfaces;

namespace URLShorteningService.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken()
        {

            string urlSafe = string.Empty;

            Enumerable.Range(48, 75)
                .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
                .OrderBy(o => new Random().Next())
                .ToList()
                .ForEach(i => urlSafe += Convert.ToChar(i));

            string token = urlSafe.Substring(new Random().Next(0, urlSafe.Length), new Random().Next(2, 8));

            return token;
        }
    }
}