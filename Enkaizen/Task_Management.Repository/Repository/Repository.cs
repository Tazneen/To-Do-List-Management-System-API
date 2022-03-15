using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task_Management.Data.Data;
using Task_Management.Repository.Interfaces;

namespace Task_Management.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddRange(IList<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task Delete(object id)
        {
            var entity = await GetById(id);
            dbSet.Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    List<Expression<Func<TEntity, object>>> includeProperties = null,
                    int? page = null,
                    int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query.AsQueryable();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IList<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }
    }
}
