namespace PortalColaboradores.Core;

public class EntityException : Exception
{
    public string PropertyName { get; protected set; }
    
    public EntityException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
    
    public EntityException(string propertyName, string message, Exception inner) : base(message, inner)
    {
        PropertyName = propertyName;
    }
}