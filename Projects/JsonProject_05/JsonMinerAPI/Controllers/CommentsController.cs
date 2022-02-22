using JsonMinerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace JsonMinerAPI.Controllers
{
    public class CommentsController : ApiController
    {
        // get all the comments
        public IEnumerable<Comment> Get()
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                return entities.Comments.ToList();
            }
        }
        [Route("api/Comments/{CommentId}")] // get the comment with comment Id
        public HttpResponseMessage GetById(int CommentId)
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                var entity = entities.Comments.FirstOrDefault(e => e.CommentId == CommentId);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Comment with Id " + CommentId.ToString() + " not found");
                }
            }
        }
        [Route("api/Comments/Posts/{PostId}")] // get the all comment of the perticual post
        public HttpResponseMessage GetByPostId(int PostId)
        {
            using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
            {
                var entity = entities.Comments.FirstOrDefault(e => e.PostId == PostId);
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
        [Route("api/Comments/{PostId}")] // Post the comment by Post Id 
        public HttpResponseMessage PostByPostId(int PostId, [FromBody] Comment comment)
        {
            try
            {
                using (JsonMinerDbEntities entitie = new JsonMinerDbEntities())
                {
                    var entity = entitie.Posts.FirstOrDefault(e => e.PostId == PostId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Post Id with Id " + PostId.ToString() + " not found to Post");
                    }
                    else
                    {
                        comment.PostId = PostId;
                        using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                        {
                            entities.Comments.Add(comment);
                            entities.SaveChanges();
                            var message = Request.CreateResponse(HttpStatusCode.Created, comment);
                            message.Headers.Location = new Uri(Request.RequestUri +
                                comment.CommentId.ToString());
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
        [Route("api/Comments/{CommentId}")] // Delete the comment by comment Id
        public HttpResponseMessage DeleteById(int CommentId)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Comments.FirstOrDefault(e => e.CommentId == CommentId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Comment with Id = " + CommentId.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Comments.Remove(entity);
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
        [Route("api/Comments/{CommentId}")] // Put the commetn by Comment Id
        public HttpResponseMessage PutById(int CommentId, [FromBody] Comment comment)
        {
            try
            {
                using (JsonMinerDbEntities entities = new JsonMinerDbEntities())
                {
                    var entity = entities.Comments.FirstOrDefault(e => e.CommentId == CommentId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Comment with Id " + CommentId.ToString() + " not found to update");
                    }
                    else
                    {
                        if (comment.UserId == entity.UserId)
                        {
                            entity.Body = comment.Body;
                        entity.Date = comment.Date;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                "User with Id " + comment.UserId.ToString() + " not found to update");
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
