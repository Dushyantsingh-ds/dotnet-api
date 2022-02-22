using JsonMinerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace JsonMinerAPI.Controllers
{
    public class PostsController : ApiController
    {
        public IEnumerable<Post> Get()
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                return entities.Posts.ToList();
            }
        }
        [Route("api/posts/{PostId}")]
        public HttpResponseMessage Get(int PostId)
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                var entity = entities.Posts.FirstOrDefault(e => e.PostId == PostId);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Post with Id " + PostId.ToString() + " not found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Post post)
        {
            try
            {
                using (JsonMinerDbEntities entitie = new JsonMinerDbEntities())
                {
                    var entity = entitie.Users.FirstOrDefault(e => e.UserId == post.UserId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + post.UserId.ToString() + " not found to Post");
                    }
                    else
                    {
                        using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                        {
                            entities.Posts.Add(post);
                            entities.SaveChanges();
                            var message = Request.CreateResponse(HttpStatusCode.Created, post);
                            message.Headers.Location = new Uri(Request.RequestUri +
                                post.PostId.ToString());
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
        [Route("api/posts/{PostId}")]
        public HttpResponseMessage Delete(int PostId)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Posts.FirstOrDefault(e => e.PostId == PostId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id = " + PostId.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Posts.Remove(entity);
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
        [Route("api/posts/{PostId}")]
        public HttpResponseMessage Put(int PostId, [FromBody] Post post)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Posts.FirstOrDefault(e => e.PostId == PostId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + PostId.ToString() + " not found to update");
                    }
                    else
                    {
                        if (post.UserId == entity.UserId)
                        {
                        entity.Title = post.Title;
                        entity.Body = post.Body;
                        entity.Category = post.Category;
                        entity.Date = post.Date;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                "User with Id " + post.UserId.ToString() + " not found to update");
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
