using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
