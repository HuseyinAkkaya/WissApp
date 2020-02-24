using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WissAppWebApi //using ile almamak için nameSpace değiştirildi
{
    public class Mapping
    {
        public static readonly Mapper mapper;

        static Mapping()
        {
            MappingConfig mappingConfig = new MappingConfig();
            mapper = new Mapper(MappingConfig.mapperConfiguration);

        }
    }
}