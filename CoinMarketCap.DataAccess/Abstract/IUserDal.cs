using CoinMarketCap.Core.DataAccess;
using CoinMarketCap.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
