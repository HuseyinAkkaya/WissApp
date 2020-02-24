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
using WissAppEF.Contexts;
using WissAppEntities.Entities;

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
                return Ok(userService.GetEntities());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}
