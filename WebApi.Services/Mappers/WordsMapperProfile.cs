using AutoMapper;
using WebApi.Domain.Entities;
using WebApi.Domain.Models;

namespace WebApi.Services.Mappers;

public sealed class WordsMapperProfile: Profile
{
    public WordsMapperProfile()
    {			
        CreateMap<WordEntity, WordModel>().ReverseMap();
        CreateMap<WordEntity, CreateWordCommand>()
            .ReverseMap()
            .ForMember(x=>x.Id, opt => opt.Ignore());
    }
}