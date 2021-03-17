namespace URLShorteningService.DataAccess.Interfaces
{
    public interface IDataAccess
    {
        string GetShortUrl(string longUrl);

        (bool exists, string shortUrl) CheckTokenExists(string token);

        string AddShortUrl(string url);
    }
}