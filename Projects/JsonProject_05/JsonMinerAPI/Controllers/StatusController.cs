using JsonMinerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace JsonMinerAPI.Controllers
{
        public class StatusController : ApiController
        {
        public HttpResponseMessage Get()
        {         
            return Request.CreateResponse(HttpStatusCode.OK, "Connection successful!" + DateTime.Now + "  ");
        }   
    }
}
