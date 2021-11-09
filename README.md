## DotNet Projects API's Practice

## .Net Web API Projects
-1 [Simple web api Project](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03)

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
  
  -----
  
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

### Projects:
-1 [Jsonplaceholder Sample API call](https://github.com/Dushyantsingh-ds/dotnet-api/tree/main/Projects/WebApplication_project_03)
