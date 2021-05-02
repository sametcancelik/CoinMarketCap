using CoinMarketCap.Business.Concrete.DTOs;
using CoinMarketCap.Core.Utilities.Results;
using CoinMarketCap.Core.Utilities.Security.JWT;

namespace CoinMarketCap.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserDetailDto> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<UserDetailDto> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(UserDto user);
    }
}
