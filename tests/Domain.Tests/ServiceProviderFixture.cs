using Domain.Models;
using Domain.Rules.Calculator;
using Domain.Rules.CarRules;
using Domain.Rules.DateRules;
using Domain.Rules.FeeRules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace Domain.Tests
{
    public class ServiceProviderFixture
    {
        public ServiceProviderFixture()
        {
            var dir = Directory.GetCurrentDirectory();
            Configuration = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<List<FeeSlot>>(
                options => Configuration.GetSection("FeeSlots")
                .Bind(options));
            serviceCollection.AddScoped<CalculatorRulesEngine>();
            serviceCollection.AddScoped<FeeRulesEngine>();
            serviceCollection.AddScoped<DateRulesEngine>();
            serviceCollection.AddScoped<CarRulesEngine>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
    }
}
