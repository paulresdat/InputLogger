using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConsoleApp.Domain.Dtos;
using ConsoleApp.Domain.Entities;

namespace ConsoleApp.Domain.Repositories
{
    public interface IConsoleAppRepository
    {
        // crud
        UserInputLogDto CreateUserInputLog(UserInputLogDto userInputLog);
        List<UserInputLogDto> GetLogs(); // read
        void UpdateUserInputLog(UserInputLogDto userInputLog);
        void DeleteUserInputLog(int logId);
        void DeleteLogs();
    }

    public class ConsoleAppRepository : IConsoleAppRepository
    {
        private readonly ConsoleAppDbContext _dbContext;
        private readonly MapperConfiguration _mapperConfiguration;
        public ConsoleAppRepository(ConsoleAppDbContext dbContext, MapperConfiguration mapperConfig)
        {
            _dbContext = dbContext;
            _mapperConfiguration = mapperConfig;
        }

        public UserInputLogDto CreateUserInputLog(UserInputLogDto userInputLog)
        {
            var log = new UserInputLog
            {
                // UserId = userInputLog.UserId.Value,
                UserInput = userInputLog.UserInput,
                CreatedBy = userInputLog.CreatedBy,
                CreatedDate = userInputLog.CreatedDate.Value,
            };

            _dbContext.UserInputLogs.Add(log);
            _dbContext.SaveChanges();

            var logDto = _dbContext.UserInputLogs
                .Where(x => x.UserInputLogId == log.UserInputLogId)
                .ProjectTo<UserInputLogDto>(_mapperConfiguration)
                .First();

            return logDto;
        }

        public List<UserInputLogDto> GetLogs()
        {
            return _dbContext.UserInputLogs
                .ProjectTo<UserInputLogDto>(_mapperConfiguration)
                .ToList();
        }

        public void DeleteLogs()
        {
            var logs = _dbContext.UserInputLogs.ToList();
            _dbContext.UserInputLogs.RemoveRange(logs);
            _dbContext.SaveChanges();
        }

        public void UpdateUserInputLog(UserInputLogDto userInputLog)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserInputLog(int logId)
        {
            throw new System.NotImplementedException();
        }
    }
}
