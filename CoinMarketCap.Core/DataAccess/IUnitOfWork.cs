using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();

        void Rollback();
    }
}
