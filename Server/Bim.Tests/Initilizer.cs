namespace Bim.Tests
{
    [TestClass]
    internal class Initializer
    {
        public static IServiceProvider _serviceProvider { get; protected set; }
        public static Client _client { get; protected set; }

        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var factory = new TestApplicationFactory();
            var httpClient = factory.CreateDefaultClient();

            _client = new Client(httpClient.BaseAddress.ToString(), httpClient);
            _serviceProvider = factory.Services;
        }

    }
}