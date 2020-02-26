using AppCore.Services;
using AppCore.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    public class UserMessagesController : ApiController
    {
        private DbContext db = new WissAppContext();
        private ServiceBase<UsersMessages> service;

        public UserMessagesController()
        {
            var service = new Service<UsersMessages>(db);
        }
        
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {

                var entities = service
                    .GetEntityQuery(e => e.SenderId == id || e.ReceiverId == id)
                    .OrderBy(e => e.Messages.Date);
                
                return Ok(entities.ProjectTo<UsersMessagesModel>(MappingConfig.mapperConfiguration).ToList());
            }
            catch (System.Exception exc)
            {

                return BadRequest();
            }


        }


    }
}
