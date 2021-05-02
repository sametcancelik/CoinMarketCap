using CoinMarketCap.Core.DataAccess.Concrete.EntityFrameworkCore;
using CoinMarketCap.Core.Entities.Concrete;
using CoinMarketCap.DataAccess.Abstract;
using CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore
{
    public class EFUserDal : EFEntityRepositoryBase<User,CoinMarketCapContext>, IUserDal
    {
        private readonly CoinMarketCapContext _context;
        public EFUserDal(CoinMarketCapContext context):base(context)
        {
            _context = context;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in _context.OperationClaims
                         join userOperationClaim in _context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
        }
    }
}
