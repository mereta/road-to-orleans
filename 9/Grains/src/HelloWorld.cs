using System.Threading.Tasks;
using Interfaces;
using Orleans;
using Library;

namespace Grains
{
    public class HelloWorld : Orleans.Grain, IHelloWorld
    {
        private readonly IHelloService _helloService;

        public HelloWorld(IHelloService helloService)
        {
            _helloService = helloService;
        }

        public async Task<string> SayHello(string name, GrainCancellationToken grainCancellationToken)
        {
            string result = null;

            if (!grainCancellationToken.CancellationToken.IsCancellationRequested)
            {
                return await Task.FromResult(_helloService.Say(name));
            }

            return result;
        }
    }
}