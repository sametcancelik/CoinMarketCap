﻿using CoinMarketCap.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Business.Concrete.DTOs
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
