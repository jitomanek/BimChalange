using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Bim.Tests
{
    internal class TestApplicationFactory : WebApplicationFactory<Bim.Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {

            //override settings and configuration here
            builder.ConfigureServices((hostBuilder, services) =>
            {
               //RestApi already uses InMemory DB
            });

            builder.UseEnvironment("Test");
            return base.CreateHost(builder);
        }
    }
}
