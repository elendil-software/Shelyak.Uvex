namespace Shelyak.Usis.Responses;

public interface IResponseParser
{
    IResponse<T> Parse<T>(string responseString);
}