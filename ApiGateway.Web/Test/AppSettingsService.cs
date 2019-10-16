using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Web.Test
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IOptionsMonitor<TestSettings> _setting;

        public AppSettingsService(IOptionsMonitor<TestSettings> setting)
        {
            _setting = setting;
        }

        public string GetMessage()
        {
            return _setting.CurrentValue.Message;
        }
    }
}
