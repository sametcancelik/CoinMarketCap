using CoinMarketCap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoinMarketCap.Core.DataAccess.Concrete.EntityFrameworkCore
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
         where TEntity : class, IEntity, new()
         where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFEntityRepositoryBase(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter).FirstOrDefault();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return _dbSet.Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
