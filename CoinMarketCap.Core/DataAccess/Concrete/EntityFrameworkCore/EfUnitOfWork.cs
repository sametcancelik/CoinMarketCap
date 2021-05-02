using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinMarketCap.Core.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public EfUnitOfWork(TContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
