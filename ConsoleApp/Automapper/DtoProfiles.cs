using AutoMapper;
using ConsoleApp.Domain.Dtos;
using ConsoleApp.Domain.Entities;

namespace ConsoleApp.Automapper
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            CreateMap<UserInputLog, UserInputLogDto>();
        }
    }
}
