using AutoMapper;
using Newtonsoft.Json;
using Sms.Api.ViewModels;
using Sms.Data.Entities;
using System;
using System.Collections.Generic;

namespace Sms.Api.Services.MapperConfigs
{
    public static class DefaultMapper
    {
        public static Action<IMapperConfigurationExpression> Config = (IMapperConfigurationExpression config) =>
        {
            // not sure what else should go here
            config.ValidateInlineMaps = false;
            config.CreateMap<Student, StudentViewModel>()
            .ForMember(
                dest => dest.FavoriteBooksList, 
                src => src.MapFrom(opts => JsonConvert.DeserializeObject<ICollection<BookViewModel>>(opts.FavoriteBooksList)));
            config.CreateMap<StudentViewModel, Student>()
            .ForMember(
                dest => dest.FavoriteBooksList, 
                src => src.MapFrom(opts => JsonConvert.SerializeObject(opts.FavoriteBooksList)));
        };
    }
}
