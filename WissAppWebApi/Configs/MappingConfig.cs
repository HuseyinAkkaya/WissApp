using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi
{
    public class MappingConfig
    {
        public static readonly MapperConfiguration mapperConfiguration;

        static MappingConfig()
        {
            mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile<UsersProfile>();
                    c.AddProfile<UsersModelProfile>();
                    c.AddProfile<UsersMessagesProfile>();
                });
        }

    }

    class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //CreateMap<Users, UsersModel>().ReverseMap();//ReverseMap iki yönlü dönüşüm sağlar.
            CreateMap<Users, UsersModel>().
                ForMember(d => d.Password, o => o.Ignore()).
                ForMember(d => d.Role, o => o.MapFrom(s => s.Roles.Name));

        }
    }

    class UsersModelProfile : Profile
    {
        public UsersModelProfile()
        {
            CreateMap<UsersModel, Users>();
        }
    }
    class UsersMessagesProfile : Profile
    {
        public UsersMessagesProfile()
        {
            CreateMap<UsersMessages, UsersMessagesModel>()
                .ForMember(d => d.Message, o => o.MapFrom(s => s.Messages.Message))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Messages.Date))
                .ForMember(d => d.Receiver, o => o.MapFrom(s => s.Receiver.UserName))
                .ForMember(d => d.Sender, o => o.MapFrom(s => s.Sender.UserName));

        }
    }

}