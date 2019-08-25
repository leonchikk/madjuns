using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Extensions
{
    public static class ServiceBusExtentions
    {
        private static ServiceBusListener _listener { get; set; }

        public static IApplicationBuilder UseServiceBusListener(this IApplicationBuilder app)
        {
            _listener = (ServiceBusListener)app.ApplicationServices.GetService(typeof(ServiceBusListener));

            var lifetime = (IApplicationLifetime)app.ApplicationServices.GetService(typeof(IApplicationLifetime));
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _listener.SubscribeToEvents();
        }

        private static void OnStopping()
        {
            _listener.Dispose();
        }
    }
}
