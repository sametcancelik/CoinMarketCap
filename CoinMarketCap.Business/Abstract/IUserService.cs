using CoinMarketCap.Business.Concrete.DTOs;
using CoinMarketCap.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(UserDto user);
        void Add(UserDto user);
        UserDto GetByMail(string email);
    }
}
