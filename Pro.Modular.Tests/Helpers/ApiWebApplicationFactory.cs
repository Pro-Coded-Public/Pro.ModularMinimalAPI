using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Pro.Modular.Tests.Helpers;
internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureAppConfiguration(_ => { });
        _ = builder.ConfigureTestServices(_ => { });
    }
}