using System.Net;
namespace Unitester_Domain.Exceptions;

public class TooManyRequestException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;

    public string TitleMessage { get; protected set; } = String.Empty;
}
