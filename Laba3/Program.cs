using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Laba3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseKestrel(options =>
                   {
                       options.Limits.MaxConcurrentConnections = 100;
                       options.Limits.MaxRequestBodySize = 10 * 1024;
                       options.Limits.MinRequestBodyDataRate =
                           new MinDataRate(100, TimeSpan.FromSeconds(10));
                       options.Limits.MinResponseDataRate =
                           new MinDataRate(100, TimeSpan.FromSeconds(10));
                       options.Listen(IPAddress.Loopback, 5000);
                   })
                   .Build();
    }
}
