using Unitester_Service.Dtos.Notification;
namespace Unitester_Service.Interfaces.Notification;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
