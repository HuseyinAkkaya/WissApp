using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Services.Description;
using AppCore.Services;
using AppCore.Services.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    public class UsersController : ApiController
    {
        private DbContext db;
        private ServiceBase<Users> userService;

        public UsersController()
        {
            db = new WissAppContext();
            userService = new Service<Users>(db);
            
        }

        public IHttpActionResult Get()
        {
            try
            {
                var entities = userService.GetEntities();
               // var model = Mapping.mapper.Map<List<Users>,List<UsersModel>>(entities);
               var model = userService.GetEntityQuery().ProjectTo<UsersModel>(MappingConfig.mapperConfiguration).ToList();
                //var model = Mapping.mapper.Map<List<UsersModel>>(entities);

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var entitie = userService.GetEntity(id);
                var model = Mapping.mapper.Map<UsersModel>(entitie);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        public IHttpActionResult Post(UsersModel usersModel)
        {
            try
            {
                var entity = Mapping.mapper.Map<Users>(usersModel);
                userService.AddEntity(entity);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}
