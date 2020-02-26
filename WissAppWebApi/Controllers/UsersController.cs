using AppCore.Services;
using AppCore.Services.Base;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Attributes;
using WissAppWebApi.Configs;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    //[ClaimAuth("role", "admin,moderator")]
    [ClaimAuth("role", "admin")]
    [RoutePrefix("api/Users")]
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

        //[AllowAnonymous]
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
                entity.IsActive = true;
                userService.AddEntity(entity);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Put(UsersModel usersModel)
        {
            try
            {
                var entity = userService.GetEntity(usersModel.Id);

                entity.BirthDate = usersModel.BirthDate;
                entity.Email = usersModel.Email;
                entity.Gender = usersModel.Gender;
                entity.IsActive = usersModel.IsActive;
                entity.Location = usersModel.Location;
                entity.Password = usersModel.Password;
                entity.RoleId = usersModel.RoleId;
                entity.School = usersModel.School;
                entity.UserName = usersModel.UserName;
                userService.UpdateEntity(entity);

                var model = Mapping.mapper.Map<UsersModel>(entity);

                return Ok(model);

            }
            catch (Exception exc)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Delete(int id)
        {


            try
            {
                var entity = userService.GetEntity(id);
                userService.DeleteEntity(entity);
                var model = Mapping.mapper.Map<UsersModel>(entity);

                return Ok(model);
            }
            catch (Exception exc)
            {

                return BadRequest();
            }
        }

        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {

            try
            {
                var entities = userService.GetEntities();

                var resultEntites = JsonConvert.SerializeObject(entities, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                var model = JsonConvert.DeserializeObject(resultEntites);

                return Ok(model);
            }
            catch (Exception exc)
            {

                return BadRequest();
            }
        }

        [Route("Logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            var principle = RequestContext.Principal as ClaimsPrincipal;
            if (principle.Identity.IsAuthenticated)
            {
                UserConfig.AddLoggedOutUser(principle.FindFirst(e => e.Type == "user").Value);
                return Ok("User logged out");
            }

            return BadRequest("User didn't login!");

        }


        [Route("LoggedOutUsers")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult LogedOutUsers()
        {
            return Ok(UserConfig.GetLoggedOutUser());
        }

       




        [Route("Sendmail")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult SendMail()
        {
            try
            {
                var sc = new System.Net.Mail.SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = "smtp.live.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Timeout = 20000,
                    Credentials = new NetworkCredential(@"mail", @"password")
                };
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("kadioglu50@hotmail.com", "Hüseyin AKKAYA");

                mail.To.Add("huseyin.akkaya93@gmail.com");
                //mail.To.Add("alici2@mail.com");

                //mail.CC.Add("alici3@mail.com");
                //mail.CC.Add("alici4@mail.com");

                mail.Subject = "E-Posta Konusu";
                mail.IsBodyHtml = true;
                mail.Body = "E-Posta İçeriği";

                //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
                //mail.Attachments.Add(new Attachment(@"C:\Sonuc.pptx"));

                sc.Send(mail);
                return Ok("OK");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("OK" + e);

            }


        }



    }
}
