using CoinMarketCap.Business.Abstract;
using CoinMarketCap.Business.Concrete.DTOs;
using CoinMarketCap.Core.Entities.Concrete;
using CoinMarketCap.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(UserDto user)
        {
            var userEntity = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };

            _userDal.Add(userEntity);
        }

        public UserDto GetByMail(string email)
        {
            var user = _userDal.Get(x => x.Email == email);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    Status = user.Status
                };
            }
            return null;
        }

        public List<OperationClaim> GetClaims(UserDto user)
        {
            return _userDal.GetClaims(new User { Id = user.Id });
        }
    }
}
