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
                });
        }

    }

    class UsersProfile:Profile
    {
        public UsersProfile()
        {
            //CreateMap<Users, UsersModel>().ReverseMap();//ReverseMap iki yönlü dönüşüm sağlar.
            CreateMap<Users, UsersModel>().ForMember(e => e.Password, o => o.Ignore()).ReverseMap();//ReverseMap iki yönlü dönüşüm sağlar.
           
        }
    }

    class UsersModelProfile : Profile
    {
        public UsersModelProfile()
        {
            CreateMap<UsersModel, Users>();
        }
    }

}