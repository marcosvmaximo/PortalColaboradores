namespace PortalColaboradores.Core.NotificationPattern;

public interface INotifyHandler
{
    Task PublicarNotificacao<T>(T notificacao) where T : Notification;
    Task<bool> AnyNotifications();
    Task<IEnumerable<Notification>> GetNotifications();
}