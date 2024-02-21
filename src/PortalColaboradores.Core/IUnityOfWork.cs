namespace PortalColaboradores.Core;

public interface IUnityOfWork
{
    Task<bool> Commit();
}