using System.Net;
namespace Unitester_Domain.Exceptions;

public class ExpiredException:Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.Gone;

    public string TitleMessage { get; protected set; } = String.Empty;
}
