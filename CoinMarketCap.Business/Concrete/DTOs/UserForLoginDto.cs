using CoinMarketCap.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Business.Concrete.DTOs
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
