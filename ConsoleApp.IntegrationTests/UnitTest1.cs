using System;
using AutoMapper;
using AutoMapper.Configuration;
using ConsoleApp.Automapper;
using ConsoleApp.Domain;
using ConsoleApp.Domain.Dtos;
using ConsoleApp.Domain.Entities;
using ConsoleApp.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ConsoleApp.IntegrationTests
{
    public abstract class BaseUnitTest
    {
        protected IServiceCollection ServiceCollection { get; set; }
        protected IServiceProvider GetNewServiceProvider => ServiceCollection.BuildServiceProvider();

        protected BaseUnitTest()
        {
            ServiceCollection = new ServiceCollection();
        }

        protected void AddMapper(Action<IMapperConfigurationExpression> profileAction)
        {
            var configMapper = new MapperConfiguration(profileAction);
            var mapper = new Mapper(configMapper);
            ServiceCollection.AddSingleton(configMapper);
            ServiceCollection.AddSingleton(mapper);
        }
    }

    public class UnitTest1 : BaseUnitTest, IDisposable
    {
        private readonly IServiceProvider _sp;
        private readonly ConsoleAppDbContext _dbContext;

        public UnitTest1()
        {
            AddMapper(cfg =>
            {
                cfg.AddProfile<DtoProfiles>();
            });
            ServiceCollection.AddSingleton<ConsoleAppDbContext>();
            ServiceCollection.AddSingleton<IConsoleAppRepository, ConsoleAppRepository>();
            _sp = GetNewServiceProvider;
            _dbContext = _sp.GetRequiredService<ConsoleAppDbContext>();
            _dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _dbContext.Database.RollbackTransaction();
        }

        [Fact]
        public void Test1()
        {
            var repo = _sp.GetRequiredService<IConsoleAppRepository>();
            var userInput = new UserInputLogDto
            {
                UserInput = "test 123",
                CreatedBy = "xunit",
                CreatedDate = DateTime.Now,
            };

            var dto = repo.CreateUserInputLog(userInput);

            Assert.Equal(userInput.UserInput, dto.UserInput);
            Assert.Equal(userInput.CreatedBy, dto.CreatedBy);
            Assert.Equal(userInput.CreatedDate, dto.CreatedDate);
        }
    }
}
