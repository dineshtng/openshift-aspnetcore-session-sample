using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreSessionSample
{
    public class Program
    {
        public static string RedisConnectionString { get; private set;}

        public static void Main(string[] args)
        {
        Console.WriteLine("Main Start ");
           var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
            var url = config["ASPNETCORE_URLS"] ?? "http://*:8080";
            RedisConnectionString = config["ASPNETCORE_Redis_Cache"];
            Console.WriteLine("RedisConnectionString: " + RedisConnectionString);
            
            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();

            host.Run();
        }
    }
}
