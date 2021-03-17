namespace URLShorteningService.Utils
{
    public interface IJsonWrapper
    {
        string JsonSerializeObject<T>(T value);

        T JsonDeserializeObject<T>(string value);
    }
}