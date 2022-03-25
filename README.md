## DotNet Projects API's Practice

## .Net Web API Projects
-1 [Simple web api Project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03) <br/>
-2 [Empolyee Web api Project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/EmployeeService_project_04) <br/>
-3 [Semi Advance web api project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/JsonProject_05) <br/>
-4 [Token Based Auth](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/TokenBasedAuthWebApp) <br/>


## HTTP verbs
<details>
  <summary>Click to expand!</summary>
  
|HTTP Verb	| CRUD	| Entire Collection (e.g. /customers)	| Specific Item (e.g. /customers/{id}) |
|---|---|---|---|
|POST |Create |	201 (Created), 'Location' header with link to /customers/{id} containing new ID. |	404 (Not Found), 409 (Conflict) if resource already exists..
| GET | Read |	200 (OK), list of customers. Use pagination, sorting and filtering to navigate big lists. |	200 (OK), single customer. 404 (Not Found), if ID not found or invalid.
| PUT |	Update/Replace |	405 (Method Not Allowed), unless you want to update/replace every resource in the entire collection.	200 (OK) or 204 (No Content). 404 (Not Found), if ID not found or invalid.
| PATCH |	Update/Modify	| 405 (Method Not Allowed), unless you want to modify the collection itself.	| 200 (OK) or 204 (No Content). 404 (Not Found), if ID not found or invalid.
| DELETE |	Delete	| 405 (Method Not Allowed), unless you want to delete the whole collection—not often desirable.	200 (OK). | 404 (Not Found), if ID not found or invalid.
</details>

## Docs 
<details>
  <summary>Click to expand!</summary>
-1 Web API Content Negotiation <br/><br/>

Accetpt:application/xml <br/>
Accetpt:application/json <br/>
  
## jsonpFormatter  
<details>
  <summary>Click to expand!</summary>
  
## -1 Install NuGet Package.  <br/>
  
 ### 1.1-Package Manager Console. <br>
  Use this cmd in VS Terminal <br>
  ``` Install-Package WebApiContrib.Formatting.Jsonp ``` 
## 2 Add NameSpace
  ``` using WebApiContrib.Formatting.Jsonp; ```
## 3 Edit Config file <br>
  
Open the file App_Start/WebApiConfig.cs. Add the following code to the WebApiConfig.Register method: 
  ```
var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
config.Formatters.Insert(0, jsonpFormatter);
  ```
  
  From this 
  ```
   public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
  ```
  
  To this
  ```
    public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, jsonpFormatter); 
        }
  ```
</details>
</details>

------------------

# Methods 
## Default Methods
<details>
  <summary>Click to expand!</summary>
  
  ```
  public class ValuesController : ApiController
{
    static List<string> strings = new List<string>()
    {
        "value0", "value1", "value2"
    };
    // GET api/values
    public IEnumerable<string> Get()
    {
        return strings;
    }

    // GET api/values/5
    public string Get(int id)
    {
        return strings[id];
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
        strings.Add(value);
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
        strings[id] = value;
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
        strings.RemoveAt(id);
    }
}
```

  </details>

## Methods for EntityFramework
  <details>
  <summary>Click to expand!</summary>
    
-----
### Get 
<details>
  <summary>Click to expand!</summary>
  
  ``` 
   public IEnumerable<Employee> Get()
        {
            using(EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }
  ```
  
  </details>
  
 ### Get (int Id)
<details>
  <summary>Click to expand!</summary>
  
  ``` 
   public Employee Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.ID == id);
            }
        }
  ```
  </details> 
  
   ### Get (int Id) [HttpResponseMessage]
<details>
  <summary>Click to expand!</summary>
  
  ``` 
public HttpResponseMessage Get(int id)
{
    using (EmployeeDBEntities entities = new EmployeeDBEntities())
    {
        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        if (entity != null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                "Employee with Id " + id.ToString() + " not found");
        }
    }
}
  ```
  </details> 
  
  -----
  
  ### Post ([FromBody] Employee employee)
<details>
  <summary>Click to expand!</summary>
  
  ``` 
   public void Post([FromBody] Employee employee)
{
    using (EmployeeDBEntities entities = new EmployeeDBEntities())
    {
        entities.Employees.Add(employee);
        entities.SaveChanges();
    }
}
  ```
  </details> 
  
   ### Post ([FromBody] Employee employee) [HttpResponseMessage]
<details>
  <summary>Click to expand!</summary>
  
  ``` 
 public HttpResponseMessage Post([FromBody] Employee employee)
{
    try
    {
        using (EmployeeDBEntities entities = new EmployeeDBEntities())
        {
            entities.Employees.Add(employee);
            entities.SaveChanges();

            var message = Request.CreateResponse(HttpStatusCode.Created, employee);
            message.Headers.Location = new Uri(Request.RequestUri +
                employee.ID.ToString());

            return message;
        }
    }
    catch (Exception ex)
    {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
    }
}
  ```
  </details> 
  
   -----
  
  ### Delete (int id)
<details>
  <summary>Click to expand!</summary>
  
  ``` 
   public void Delete(int id)
{
    using (EmployeeDBEntities entities = new EmployeeDBEntities())
    {
        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
        entities.SaveChanges();
    }
}

  ```
  </details> 
  
  ### Delete (int id) [HttpResponseMessage]
<details>
  <summary>Click to expand!</summary>
  
  ``` 
  public HttpResponseMessage Delete(int id)
{
    try
    {
        using (EmployeeDBEntities entities = new EmployeeDBEntities())
        {
            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Employee with Id = " + id.ToString() + " not found to delete");
            }
            else
            {
                entities.Employees.Remove(entity);
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
  ```
  </details> 
  
   -----
  
  ### Put (int id, [FromBody]Employee employee)
<details>
  <summary>Click to expand!</summary>
  
  ``` 
  public void Put(int id, [FromBody]Employee employee)
{
    using (EmployeeDBEntities entities = new EmployeeDBEntities())
    {
        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

        entity.FirstName = employee.FirstName;
        entity.LastName = employee.LastName;
        entity.Gender = employee.Gender;
        entity.Salary = employee.Salary;

        entities.SaveChanges();
    }
}
  ```
  </details> 
  
   ### Put (int id, [FromBody]Employee employee) [HttpResponseMessage]
<details>
  <summary>Click to expand!</summary>
  
  ``` 
 public HttpResponseMessage Put(int id, [FromBody]Employee employee)
{
    try
    {
        using (EmployeeDBEntities entities = new EmployeeDBEntities())
        {
            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Employee with Id " + id.ToString() + " not found to update");
            }
            else
            {
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Gender = employee.Gender;
                entity.Salary = employee.Salary;

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
  ```
  </details> 
  </details> 
  
  -----
  
  
# Method Customization  

## Query string parameters
<details>
  <summary>Click to expand!</summary>
  
   -----
  
  ### Get(string gender = "All")
<details>
  <summary>Click to expand!</summary>
  
  ``` 
  public HttpResponseMessage Get(string gender = "All")
{
    using (EmployeeDBEntities entities = new EmployeeDBEntities())
    {
        switch (gender.ToLower())
        {
            case "all":
                return Request.CreateResponse(HttpStatusCode.OK,
                    entities.Employees.ToList());
            case "male":
                return Request.CreateResponse(HttpStatusCode.OK,
                    entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
            case "female":
                return Request.CreateResponse(HttpStatusCode.OK,
                    entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
            default:
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Value for gender must be Male, Female or All. " + gender + " is invalid.");
        }
    }
}
  ```
  </details> 
  </details>
  
  -----
  

    
## cross-origin requests  
<details>
  <summary>Click to expand!</summary>
  
## -1 Install NuGet Package.  <br/>
  
 ### 1.1-Package Manager Console. <br>
  Use this cmd in VS Terminal <br>
  ``` Install-Package Microsoft.AspNet.WebApi.Cors ``` 
## 2 Include the following 2 lines of code in Register() method of WebApiConfig class in WebApiConfig.cs file in App_Start folder
  ``` 
  EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
  config.EnableCors(); 
  ```
## Code Demo
  
  From this 
  ```
   public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
  ```
  
  To this
  ```
    public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();
        }
  ```
</details>
       
## Modify Token Response  
<details>
  <summary>Click to expand!</summary>

## 1 Update the following code in CreateProperties() method class in ApplicationOAuthProvider.cs file in Provider folder
## Code Demo
  
  From this 
  ```
     public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
  ```
  
  To this
  ```
     public static AuthenticationProperties CreateProperties(string userName)
        {
            AchievedItDBEntities entities = new AchievedItDBEntities();

            var UserId = entities.AspNetUsers.Where(r => r.UserName == userName).FirstOrDefault().Id;
            var RoleId = entities.AspNetUserRoles.Where(r => r.UserId == UserId).FirstOrDefault().RoleId;
            var Role = entities.AspNetRoles.Where(r => r.Id == RoleId).FirstOrDefault().Name;
            string redirectURL = "";
            if (Role == "SuperAdmin".ToUpper())
            {
                redirectURL = "/SuperAdmin";
            }
            else if (Role == "Admin".ToUpper())
            {
                redirectURL = "/Admin";
            }
            else if (Role == "Verifier".ToUpper())
            {
                redirectURL = "/Verifier";
            }
            else if (Role == "User")
            {
                redirectURL = "/User";
            }
            else
            {
                redirectURL = "Invaild";
            }
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "redirectURL", redirectURL}
            };
            return new AuthenticationProperties(data);
        }
  ```
## Request Url Post Method
  ``` https://localhost:44338/token ```
## Body  
  ``` Username=admin4@test.com&Password=Abc123@Xxx&grant_type=password ```
## API Response
  ```
  {
  "access_token":"Wm-2lne4f82ZFOcztiE_IjLyEq5MakfZw03kevDpxv_56fOIUX-j35cmizo98i9OjmcBiddJqLDKYCOqJc1QpFDL1XXY16CCFJpBx9Icg3ZZAeTX83Ii2uIolUjEy--KlaDrDE7oUCllZur8v-HyD91Q8sYS25X-tgeefYAGW_K1smzksBsz203mvmOL9f9XGMmA5EJHIg6bFay4f_y6K1v0liWS-CZc_YvqzSCPowPX5svsRywXeJeEploYwI4Mly7axW6tKBFsmQRPv7OTjp9OQhXSpA33wAH656IRCcA1IUPEMtgM5-A9ja8QIMDtht-ZQOGTE-HPimGcn7lxLzTJP7kwqYX9DOncJBZIXKafLMxNIEQRG-s7E7NZMs3sXL5v-XvZF6N4mdR37xJ1IUw8hiNvCfk8B23BBeC4R7-Uu6r4ZRqmLb7QgqOSFP440Sa-XXtPdDCjfCQBNSDX42eGKAzOIzBH9b8XGAJZ-JYjm0Ysv7WjbPvvqWEuKrz8RZA6YYWyHEXGNkERxnkdI7sRVx7oKvbJpZ3Jkm9mQxc",
  "token_type":"bearer",
  "expires_in":1209599,
  "userName":"admin4@test.com",
  "redirectURL":"/Admin",
  ".issued":"Fri, 25 Mar 2022 20:40:25 GMT",
  ".expires":"Fri, 08 Apr 2022 20:40:25 GMT"
  }
  ```
  
  
  
</details>

### Projects:
-1 [Jsonplaceholder Sample API call](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03)
