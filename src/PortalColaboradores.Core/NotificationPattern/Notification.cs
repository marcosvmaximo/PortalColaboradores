using MediatR;

namespace PortalColaboradores.Core.NotificationPattern;

public class Notification : INotification
{
    public Notification(string property, string message)
    {
        Property = property;
        Message = message;
    }

    public string Property { get; set; }
    public string Message { get; set; }
}