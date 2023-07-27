
using System.Net;
namespace Unitester_Domain.Exceptions;

public class BadRequestException:Exception
{
    public HttpStatusCode StatusCode { get; set; }=HttpStatusCode.BadRequest;

    public string TitleMessage { get; set; } = String.Empty;

}
