## DotNet Projects API's Practice

## .Net Web API Projects
-1 [Simple web api Project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03) <br/>
-2 [Empolyee Web api Project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/EmployeeService_project_04) <br/>
-3 [Semi Advance web api project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/JsonProject_05) <br/>
-4 [Token Based Auth](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/TokenBasedAuthWebApp) <br/>

# .Net API

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
  
  ## Retrieve Data from Multiple tables
<details>
  <summary>Click to expand!</summary>
  
  ### Tabels:
  Users, <br>
  Roles, <br>
  UserRoles, <br>
  Subjects, <br>
  SubjectAuthors <br>
  
 ###  Code <br>
  Step-1: Code for Custom List <br>
  ```
   public class AuthorsList
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string UserId { get; set; }
            public string RoleId { get; set; }
            public string Username { get; set; }
            public string SubjectId { get; set; }
        }
  ```
  
  Step-2 Code for Qurey
  ```
    // GET: api/master/authors
        [AllowAnonymous]
        [Route("api/master/Authors")]
        public List<AuthorsList> GetAuthors()
        {
            AuthorsList authorlisst = new AuthorsList();
           var _autobj = (from r in entities.AspNetUserRoles
                           where r.AspNetRole.Name == "AUTHOR"
                           select new
                           {
                               r.UserId,
                               r.AspNetUser.Email,
                               r.AspNetUser.SubjectAuthors.FirstOrDefault().SubjectId,
                               r.AspNetUser.SubjectAuthors.FirstOrDefault().Subject.Name
                           }).ToList();
            List<AuthorsList> authList = new List<AuthorsList>();
            foreach (var _autitem in _autobj)
            {
                authList.Add(new AuthorsList { UserId = _autitem.UserId, Email = _autitem.Email, 
                    SubjectId = _autitem.SubjectId.ToString(), Name = _autitem.Name });
            }

            return authList;
        }
  ```
  
  Step-3: Output
  ```
  [{"$id":"1","Id":null,"Email":"alex@g.com","Name":"Dermatology","UserId":"c8e5444a-f9df-4fae-b7c3-837672316ddc","RoleId":null,"Username":null,"SubjectId":"4"},{"$id":"2","Id":null,"Email":"john@g.com","Name":"Anatomy","UserId":"e0113bbc-f5e6-4397-a329-960c8b8a4f11","RoleId":null,"Username":null,"SubjectId":"6"}]
  ```
  
  </details>
  </details>   

  

    
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
  config.EnableCors(cors); 
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
            config.EnableCors(cors);
        }
  ```
</details>

## cross-origin .Net core requests  
<details>
  <summary>Click to expand!</summary>
  
## 1 Include the following 2 lines of code in Register() method of WebApiConfig class in WebApiConfig.cs file in App_Start folder
  ``` 
var myOrigins = "corspolicy";

builder.Services.AddCors(p => p.AddPolicy(myOrigins, build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

app.UseCors(myOrigins); // enable the cors origin

  ```

## Three ways to config corse
```
// allow all domains
//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
//{
//    build.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader();
//}));
// allow pertical domains
// allow all domains
//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
//{
//    build.WithOrigins("http://localhost:4200/","http://localhost:7200/").AllowAnyMethod().AllowAnyHeader();
//}));
// allow multipule domains
```
## Code Demo
  
  From this 
  ```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

  ```
  
  To this
  ```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var myOrigins = "corspolicy";

builder.Services.AddCors(p => p.AddPolicy(myOrigins, build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(myOrigins); // enable the cors origin
app.Run();

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
</details>

## JS / Jquery / Ajax
<details>
  <summary>Click to expand!</summary>
  
 ### [Get Request] - Display Data on HTML TABLE Jquery/Ajax with Entity framewrok  
  <details>
  <summary>Click to expand!</summary>
 
HTML CODE for Table
```
  <div class=" row">
       <label id="tabledivErrorText"></label>
          <table class="table table-bordered" id="tblData">
             <thead>
               <tr class="success">
                   <th>Id</th>
                   <th>Name</th>
                   <th>Email</th>
                   <th>Phone</th>
                   <th>Age</th>
               </tr>
             </thead>
           <tbody id="tblBody"></tbody>
          </table>
  </div>
```
    
JS script

```
<script src="~/Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
       $.ajax({
        url: '/api/Member/GetFamilyMemberById/2',
        method: 'GET',
        success: function (data) {
            $('#tblBody').empty();
            $.each(data, function (index, value) {
                var row = $('<tr><td>'+ value.Id + '</td><td>'
                    + value.Name + '</td><td>'
                    + value.Email + '</td><td>'
                    + value.Phone + '</td><td>'
                    + value.Age + '</td></tr>');
                $('#tblData').append(row);
            });
        },
        error: function (jQXHR) {
            // If status code is 401, access token expired, so
            // redirect the user to the login page
            if (jQXHR.status == "401") {
                $('#tabledivErrorText').text("401");
            }
            else {
                $('#tabledivErrorText').text(jqXHR.responseText);
            }
        }
    });
  });
    </script>
```    
API Get Method
```
private PerformanceTestDBEntities entities = new PerformanceTestDBEntities();
  // GET: api/GetListMembersId
  [ResponseType(typeof(Member))]
  [HttpGet]
  [Route("api/Member/GetFamilyMemberById/{Id}")]
  public IHttpActionResult GetFamilyMember(string id)
  {
      int ids = Convert.ToInt32(id);
      try
        {
         var _autobj = (from r in entities.FamilyMemberDetails
                       where r.Member.MemberId == ids
                        select new
                          {
                            r.MemberId,
                            r.Id,
                            r.Name,
                            r.Email,
                            r.Phone,
                            r.Age
               }).OrderByDescending(a => a.Age).ToList();
            
         return Ok(_autobj);
        }
    catch (DbUpdateConcurrencyException)
     {
      if (!MemberExists(ids))
       {
           return NotFound();
       }
       else
         {
            throw;
         }
      }
 }
```
</details>
  
  ### [Post Request] - Jquery/Ajax - Create User 
  <details>
  <summary>Click to expand!</summary>
    
   ``` 
  <script type="text/javascript">
        $(document).ready(function () {

            // Save the new user details
            $('#btnRegister').click(function () {
                $.ajax({
                    url: '/api/account/register',
                    method: 'POST',
                    data: {
                        email: $('#txtEmail').val(),
                        password: $('#txtPassword').val(),
                        confirmPassword: $('#txtConfirmPassword').val()
                    },
                    success: function () {
                        $('#successModal').modal('show');
                    },
                    error: function (jqXHR) {
                        $('#divErrorText').text(jqXHR.responseText);
                    }
                });
            });
        });
    </script>
 ```
</details>
  
  ### [Post Request] - Jquery/Ajax - Login User 
  <details>
  <summary>Click to expand!</summary>
    
```
   <script type="text/javascript">
        $(document).ready(function () {

            $('#linkClose').click(function () {
                $('#divError').hide('fade');
            });

            $('#btnLogin').click(function () {
                $.ajax({
                    url: '/token',
                    method: 'POST',
                    contentType: 'application/json',
                    data: {
                        username: $('#txtUsername').val(),
                        password: $('#txtPassword').val(),
                        grant_type: 'password'
                    },
                    success: function (response) {
                        sessionStorage.setItem("accessToken", response.access_token);
                        window.location.href = "dashboard";
                    },
                    error: function (jqXHR) {
                        $('#divErrorText').text(jqXHR.responseText);
                    }
                });
            });
        });
    </script>
```    
    
</details>

### [Get Request] - DropDown List Jquery/Ajax with Entity framewrok - Select List 
  <details>
  <summary>Click to expand!</summary>
 
HTML CODE for Dropdown
```
 <label class="col-12 col-sm-3 col-form-label text-left text-sm-right">Subject (Optional)</label>
 <select class="form-control" id="departmentsDropdown" name="departmentsDropdown"></select>

```
    
JS script

```
<script src="~/Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        // Save the new user details
        App.init();

        $.ajax({
            type: "GET",
            url: "/api/subjects",
            data: "{}",
            success: function (data) {
                var s = '<option value="-1">Select a Subject</option>';
                for (var i = 0; i < data.length; i++) {
                    s += '<option value="' + data[i].SubjectId + '">' + data[i].Name + '</option>';
                    console.log(data[i].name + "------" + data[i].SubjectId);
                }
                $("#departmentsDropdown").html(s);
            }
        });
        });
    </script>
```    
API Get Method
```
private ExamAcademyEntities db = new ExamAcademyEntities();

        // GET: api/Subjects
        public IQueryable<Subject> GetSubjects()
        {
            return db.Subjects;
        }
```
</details>
  
</details>
  
### Projects:
-1 [Jsonplaceholder Sample API call](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03)
