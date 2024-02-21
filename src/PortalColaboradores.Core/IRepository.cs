using System.Linq.Expressions;

namespace PortalColaboradores.Core;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    IUnityOfWork UnityOfWork { get; }
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> GetById(int id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}