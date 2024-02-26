using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Authentication.Api.Tests
{
    public class WebApiApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _configureServices;
        
        public WebApiApplicationFactory(Action<IServiceCollection> configureServices)
        {
            _configureServices = configureServices;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(_configureServices);
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Configurações adicionais se necessário
        }

    }
}
