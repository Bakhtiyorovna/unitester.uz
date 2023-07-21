
namespace Unitester_Service.Dtos.Notification;

public class SmsMessage
{
    public string Recipent { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
}
