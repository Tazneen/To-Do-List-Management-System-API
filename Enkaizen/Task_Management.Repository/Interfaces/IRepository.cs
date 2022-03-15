using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        Task<TEntity> GetById(object id);

        Task Add(TEntity entity);
        Task AddRange(IList<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IList<TEntity> entities);

        Task Delete(object id);
        void DeleteRange(IList<TEntity> entities);
    }
}
