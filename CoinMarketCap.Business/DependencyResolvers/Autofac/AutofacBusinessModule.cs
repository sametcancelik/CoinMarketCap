using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CoinMarketCap.Business.Abstract;
using CoinMarketCap.Business.Concrete;
using CoinMarketCap.Core.DataAccess;
using CoinMarketCap.Core.DataAccess.Concrete.EntityFrameworkCore;
using CoinMarketCap.Core.Utilities.Interceptors;
using CoinMarketCap.Core.Utilities.Security.JWT;
using CoinMarketCap.DataAccess.Abstract;
using CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore;
using CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore.Contexts;

namespace CoinMarketCap.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfUnitOfWork<CoinMarketCapContext>>().As<IUnitOfWork>();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EFUserDal>().As<IUserDal>().SingleInstance(); 
            
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
