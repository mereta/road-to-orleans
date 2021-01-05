using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Orleans;
using Orleans.Hosting;
using Orleans.TestingHost;
using Xunit;

namespace GrainUnitTests
{
    public class SampleUnitTest : IDisposable
    {
        private readonly TestCluster _cluster;
        public SampleUnitTest()
        {
            TestClusterBuilder builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<MySiloBuilderConfigurator>();

            _cluster = builder.Build();
            _cluster.Deploy();
        }

        public void Dispose()
        {
            _cluster.Dispose();
        }

        [Fact]
        public async Task SayHelloTest()
        {            
            var hello = _cluster.GrainFactory.GetGrain<IHelloWorld>(1);
            var mockToken = new GrainCancellationTokenSource();
            var greeting = await hello.SayHello("mike", mockToken.Token);
            Assert.Equal("Hello, mike! Your name is 4 characters long.", greeting);
        }
    }
}