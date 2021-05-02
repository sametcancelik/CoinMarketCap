using CoinMarketCap.Business.Abstract;
using CoinMarketCap.Business.Concrete.DTOs;
using CoinMarketCap.Business.Constants;
using CoinMarketCap.Core.Aspects.Autofac.Logging;
using CoinMarketCap.Core.CrossCuttingConcerns.Logging.Log4net.Loggers;
using CoinMarketCap.Core.DataAccess;
using CoinMarketCap.Core.Entities.Concrete;
using CoinMarketCap.Core.Utilities.Results;
using CoinMarketCap.Core.Utilities.Security.Hashing;
using CoinMarketCap.Core.Utilities.Security.JWT;

namespace CoinMarketCap.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<UserDetailDto> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new UserDto
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            _unitOfWork.Commit();

            return new SuccessDataResult<UserDetailDto>(new UserDetailDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email}, "Kayıt oldu");
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<UserDetailDto> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<UserDetailDto>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<UserDetailDto>("Parola hatası");
            }

            return new SuccessDataResult<UserDetailDto>(new UserDetailDto { Id = userToCheck.Id, FirstName = userToCheck.FirstName, LastName = userToCheck.LastName, Email = userToCheck.Email }, Messages.LoginSuccessfuly);
        }

        [LogAspect(typeof(FileLogger))]
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(UserDto user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(new User { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email }, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }
    }
}
