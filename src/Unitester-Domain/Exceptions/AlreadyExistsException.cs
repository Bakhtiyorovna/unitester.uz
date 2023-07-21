using System.Net;
namespace Unitester_Domain.Exceptions;

public class AlreadyExistsException :Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    public string TitleMessage { get; protected set; } = String.Empty;

}
