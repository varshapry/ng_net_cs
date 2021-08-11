using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoggingService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ng_net_cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();





            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
               .Enrich.FromLogContext()
               .Enrich.WithProperty("Application", "ng_net_cs")
               .Enrich.WithProperty("MachineName", Environment.MachineName)
               .Enrich.WithProperty("CurrentManagedThreadId", Environment.CurrentManagedThreadId)
               .Enrich.WithProperty("OSVersion", Environment.OSVersion)
               .Enrich.WithProperty("Version", Environment.Version)
               .Enrich.WithProperty("UserName", Environment.UserName)
               .Enrich.WithProperty("ProcessId", Process.GetCurrentProcess().Id)
               .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
               .WriteTo.File(formatter: new CustomTextFormatter(), path: Path.Combine(hostingContext.HostingEnvironment.ContentRootPath +
               $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}", $"load_error_{DateTime.Now:yyyyMMdd}.txt"))
               .ReadFrom.Configuration(hostingContext.Configuration));

              webBuilder.UseStartup<Startup>();
                });
    }
}
