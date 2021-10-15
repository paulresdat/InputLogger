using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using ConsoleApp.Automapper;
using ConsoleApp.Domain;
using ConsoleApp.Domain.Dtos;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var mapperConfiguration = new MapperConfiguration(mce =>
            {
                mce.AddProfile<DtoProfiles>();
            });
            var mapper = new Mapper(mapperConfiguration);
            serviceCollection.AddDbContext<ConsoleAppDbContext>();
            serviceCollection.AddSingleton(mapperConfiguration);
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddSingleton<IConsoleAppRepository, ConsoleAppRepository>();
            var sp = serviceCollection.BuildServiceProvider();
            var repo = sp.GetRequiredService<IConsoleAppRepository>();

            var quit = false;

            Console.WriteLine("CONSOLE LOGGER\n\nEverything you type gets logged to the database\n\n");
            while (!quit)
            {
                Console.Write(" $> ");
                var userInput = Console.ReadLine()?.Trim() ?? "";
                if (userInput == "q")
                {
                    quit = true;
                }
                else if (userInput == "")
                {
                    Console.WriteLine("Empty string found, ignoring");
                }
                else if (userInput == "r")
                {
                    Console.WriteLine("-------------\nFETCHING ALL LOGS\n-----------");
                    var dtos = repo.GetLogs();
                    foreach (var dto in dtos)
                    {
                        Console.WriteLine(dto.UserInput);
                    }
                }
                else if (userInput == "d")
                {
                    Console.WriteLine("------------\nDELETING ALL LOGS\n-----------");
                    repo.DeleteLogs();
                    Console.WriteLine("Deletion complete!");
                }
                else
                {
                    var dto = repo.CreateUserInputLog(new UserInputLogDto
                    {
                        UserInput = userInput,
                        CreatedBy = "consoleapp",
                        CreatedDate = DateTime.Now
                    });

                    Console.WriteLine("Log ID: " + dto.UserInputLogId + " - Logging: '" + dto.UserInput + "'");
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
