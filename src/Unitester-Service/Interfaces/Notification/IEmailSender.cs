using Unitester_Service.Dtos.Notification;
namespace Unitester_Service.Interfaces.Notification;

public interface IEmailSender
{
    public Task<bool> SenderAsync(EmailMessage emailMessage);

}
