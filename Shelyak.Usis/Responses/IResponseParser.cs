namespace Shelyak.Usis.Responses;

public interface IResponseParser
{
    IResponse Parse<T>(string responseString);
}