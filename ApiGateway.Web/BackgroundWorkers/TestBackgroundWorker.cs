using ApiGateway.Web.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.Web.BackgroundWorkers
{
    public class TestBackgroundWorker : BackgroundService
    {
        private readonly IServiceProvider _scopeProvider;
        private readonly IAppSettingsService _setting;

        public TestBackgroundWorker(IServiceProvider scope, IAppSettingsService setting)
        {
            _scopeProvider = scope;
            _setting = setting;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                //var options = _scopeProvider.GetRequiredService<IOptionsMonitor<TestSettings>>();

                while (!stoppingToken.IsCancellationRequested)
                {

                    try
                    {
                        var t = _setting.GetMessage();
                    }
                    catch (Exception e)
                    {

                    }
                }
                //await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
