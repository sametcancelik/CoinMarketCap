using CoinMarketCap.Core.Entities.Concrete;
using System.Collections.Generic;

namespace CoinMarketCap.Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
