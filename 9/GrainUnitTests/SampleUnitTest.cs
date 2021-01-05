using System;
using System.Threading.Tasks;
using Grains;
using Interfaces;
using Library;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using Orleans.TestKit;
using Orleans.TestKit.Services;
using Xunit;

namespace GrainUnitTests
{
    public class SampleUnitTest : TestKitBase
    {
        [Fact]
        public async Task SayHelloTestWithMock()
        {
            var expectedResponse = "Hello mike";
            var helloService = Silo.AddServiceProbe<IHelloService>();
            helloService.Setup(x => x.Say("mike")).Returns("Hello mike");
            var hello =  await Silo.CreateGrainAsync<HelloWorld>(1);
            var mockToken = new GrainCancellationTokenSource();
            var greeting = await hello.SayHello("mike", mockToken.Token);
            Assert.Equal(expectedResponse, greeting);
        }

        [Fact]
        public async Task SayHelloTestWithRealService()
        {
            var expectedResponse = "Hello, mike! Your name is 4 characters long.";
            Silo.AddService<IHelloService>(new HelloService());
            var hello =  await Silo.CreateGrainAsync<HelloWorld>(1);
            var mockToken = new GrainCancellationTokenSource();
            var greeting = await hello.SayHello("mike", mockToken.Token);
            Assert.Equal(expectedResponse, greeting);
            
        }
    }
}