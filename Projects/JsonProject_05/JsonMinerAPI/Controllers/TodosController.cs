using JsonMinerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace JsonMinerAPI.Controllers
{
    public class TodosController : ApiController
    {
        public IEnumerable<Todo> Get()
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                return entities.Todoes.ToList();
            }
        }
        [Route("api/Todos/{TodoId}")]
        public HttpResponseMessage Get(int TodoId)
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                var entity = entities.Todoes.FirstOrDefault(e => e.TodoId == TodoId);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Todo with Id " + TodoId.ToString() + " not found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Todo todo)
        {
            try
            {
                using (JsonMinerDbEntities entitie = new JsonMinerDbEntities())
                {
                    var entity = entitie.Users.FirstOrDefault(e => e.UserId == todo.UserId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + todo.UserId.ToString() + " not found to Post");
                    }
                    else
                    {
                        using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                        {
                            entities.Todoes.Add(todo);
                            entities.SaveChanges();
                            var message = Request.CreateResponse(HttpStatusCode.Created, todo);
                            message.Headers.Location = new Uri(Request.RequestUri +
                               todo.UserId.ToString());
                            return message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("api/Todos/{TodoId}")]
        public HttpResponseMessage Delete(int TodoId)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Todoes.FirstOrDefault(e => e.TodoId == TodoId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Todo with Id = " + TodoId.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Todoes.Remove(entity);
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
        [Route("api/Todos/{TodoId}")]
        public HttpResponseMessage Put(int PodoId, [FromBody] Todo todo)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Todoes.FirstOrDefault(e => e.TodoId == PodoId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Todo with Id " + PodoId.ToString() + " not found to update");
                    }
                    else
                    {
                        if (todo.UserId == entity.UserId)
                        {
                            entity.Title = todo.Title;
                            entity.Completed = todo.Completed;
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                "User with Id " + todo.UserId.ToString() + " not found to update");
                        }
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
