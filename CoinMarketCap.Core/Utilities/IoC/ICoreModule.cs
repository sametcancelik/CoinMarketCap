using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
