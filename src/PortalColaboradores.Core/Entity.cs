using FluentValidation;

namespace PortalColaboradores.Core;

public class Entity
{
    public Entity()
    {
    }

    public int Id { get; private set; }
    
    protected virtual bool Validate<TValidate, TEntity>()
        where TValidate : AbstractValidator<TEntity>, new()
        where TEntity : Entity
    {
        var validate = new TValidate().Validate((TEntity)this);
        if (!validate.IsValid)
        {
            foreach (var failure in validate.Errors)
            {
                throw new EntityException(failure.PropertyName, failure.ErrorMessage);
            }
        }

        return validate.IsValid;
    }
}