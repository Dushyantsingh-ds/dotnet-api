using JsonMinerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace JsonMinerAPI.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                return entities.Users.ToList();
            }
        }
        [Route("api/Users/{UserId}")]
        public HttpResponseMessage Get(int UserId)
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                var entity = entities.Users.FirstOrDefault(e => e.UserId == UserId);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "User with Id " + UserId.ToString() + " not found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        user.UserId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("api/Users/{UserId}")]
        public HttpResponseMessage Delete(int UserId)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserId == UserId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id = " + UserId.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("api/Users/{UserId}")]
        public HttpResponseMessage Put(int UserId, [FromBody] User user)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserId == UserId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + UserId.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Name = user.Name;
                        entity.Email = user.Email;
                        entity.Country = user.Country;
                        entity.Company = user.Company;
                        entity.Profession = user.Email;
                        entity.Password = user.Password;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
