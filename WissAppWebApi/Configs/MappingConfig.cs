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
                });
        }

    }

    class UsersProfile:Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UsersModel>().ReverseMap();//ReverseMap iki yönlü dönüşüm sağlar.
            
        }
    }
}