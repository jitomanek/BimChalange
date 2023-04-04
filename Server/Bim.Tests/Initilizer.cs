using Bim.Tests.Client;

namespace Bim.Tests
{
    [TestClass]
    internal class Initializer
    {
        public static IServiceProvider _serviceProvider { get; protected set; }
        public static BimClient _client { get; protected set; }

        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var factory = new TestApplicationFactory();
            var httpClient = factory.CreateDefaultClient();

            _client = new BimClient(httpClient.BaseAddress.ToString(), httpClient);
            _serviceProvider = factory.Services;
        }

    }
}