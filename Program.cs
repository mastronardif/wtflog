using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SumoLogic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace wtflog
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

public static int Main(string[] args)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.development.json")
            .Build();

            Console.WriteLine($"MMMMMMMMMMMMM {configuration["Name"]}");
            Console.WriteLine($"MMMMMMMMMMMMM {configuration["Serilog:Application"]}");
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)   
                .Enrich.FromLogContext()
                .CreateLogger(); 

/**
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            //.WriteTo.MSSqlServer(Configuration.GetConnectionString("LogConnection"), "_logs")
            .WriteTo.Console()
            //.WriteTo.ApplicationInsightsEvents("<YOUR_APPLICATION_INSIGHTS_KEY>")
            .CreateLogger();
 */
        try
        {
            Log.Information("Starting web host");

            CreateWebHostBuilder(args)
            .UseSerilog()
            .Build().Run();

            

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>() 
                .UseSerilog();
    }
}
