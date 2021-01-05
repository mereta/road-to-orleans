using GrainUnitTests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Orleans.Hosting;
using Orleans.TestingHost;

namespace Library
{
    public class MySiloBuilderConfigurator : ISiloBuilderConfigurator
    {
        public void Configure(ISiloHostBuilder hostBuilder)
        {
            //configure silo side DI with mock services
            hostBuilder.ConfigureServices(ConfigureMockServices);
        }
        
        private static void ConfigureMockServices(IServiceCollection services)
        {
            var helloService = new HelloService();

            services.AddSingleton<IHelloService>(helloService);
        }
    }

}