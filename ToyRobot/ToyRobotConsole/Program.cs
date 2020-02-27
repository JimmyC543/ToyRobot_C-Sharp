using Microsoft.Extensions.DependencyInjection;
using System;
using ToyRobotConsole.Reporter;
using ToyRobotLibrary.Robot;
using ToyRobotLibrary.RobotOperator;
using ToyRobotLibrary.Table;
using ToyRobotLibrary.ToyRobotApp;
using ToyRobotLibrary.Utilities;

namespace ToyRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup our DI
            var serviceProvider = new ServiceCollection()
                //.AddLogging()
                .AddSingleton<IToyRobotApp, ToyRobotConsoleApp>()
                .AddSingleton<IRobotOperator, RobotOperator>()
                .AddSingleton<IReporter, ConsoleReporter>()
                .AddScoped<IRobot, Robot>()
                .AddScoped<ITable>(s => new RectangularTable(5, 5))
                .BuildServiceProvider();

            // Logging isn't necessary yet, but it's handy to have
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            //var logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();
            //logger.LogDebug("Executing ToyRobot app");
            var app = serviceProvider.GetService<IToyRobotApp>();
            app.Execute();
        }
    }
}
