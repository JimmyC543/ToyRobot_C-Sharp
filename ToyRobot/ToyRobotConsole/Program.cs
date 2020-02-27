using Microsoft.Extensions.DependencyInjection;
using System;
using ToyRobotConsole.Reader;
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
                .AddSingleton<IToyRobotApp, ToyRobotConsoleApp>()
                .AddSingleton<IRobotOperator, RobotOperator>()
                .AddSingleton<IReader, ConsoleReader>()
                .AddSingleton<IReporter, ConsoleReporter>()
                .AddScoped<IRobot, Robot>()
                .AddScoped<ITable>(s => new RectangularTable(5, 5))
                .BuildServiceProvider();

            var app = serviceProvider.GetService<IToyRobotApp>();
            app.Execute();
        }
    }
}
