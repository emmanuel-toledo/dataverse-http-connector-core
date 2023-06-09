# Dataverse HTTP Connector Core

This library was created to help to the developers to create useful application connected to a Dataverse (or usually called Dynamics) environment in less time.

The package currently works using an ```Application User``` connection because this one has more request capacity. You only need to create a new ```App Registration``` resource in ```Microsoft Azure```, grant it permissions to connect to Dynamics and then register the ```Application User``` in ```Admin Power Platform``` portal.

## Getting Started

Let's see an example about how you can implement this library in an ```ASP.NET Core``` application.

### Configure connection

First of all make sure that you configure your connection inside one of the ```.json``` files. The configuration can be like the following.

```
"Dataverse": {
  "tenantId": "{ tenant unique identifier }",
  "clientId": "{ client unique identifier }",
  "clientSecret": "{ secret }",
  "resource": "{ dynamics url environment }",
  "connectionName": "{ custom identifier name }"
}
```

Each one of these values must be unique because you can configure multiple environments if you want.

```
"Dataverses": [
  {
    "tenantId": "{ tenant unique identifier }",
    "clientId": "{ client unique identifier }",
    "clientSecret": "{ secret }",
    "resource": "{ dynamics url environment }",
    "connectionName": "Org. #1"
  },
  {
    "tenantId": "{ tenant unique identifier }",
    "clientId": "{ client unique identifier }",
    "clientSecret": "{ secret }",
    "resource": "{ dynamics url environment }",
    "connectionName": "Org. #2"
  }
]
```

### Create your entity class

You also will need to create your custom classes that will be used to connect to ```Dataverse``` and use require context.

```
[EntityAttributes("ms_employee", "ms_employees")]
public class Employees
{
    [FieldAttributes("ms_EmployeeId", "ms_employeeid", FieldTypes.UniqueIdentifier)]
    public Guid Id { get; set; }

    [FieldAttributes("ms_Name", "ms_name", FieldTypes.Text)]
    public string Name { get; set; }

    [FieldAttributes("ms_Edad", "ms_edad", FieldTypes.Number)]
    public int Age { get; set; }

    [FieldAttributes("ms_Estatus", "ms_estatus", FieldTypes.OptionSet)]
    public int Status { get; set; }

    [FieldAttributes("statecode", "statecode", FieldTypes.OptionSet)]
    public int StateCode { get; set; }

    [FieldAttributes("CreatedOn", "createdon", FieldTypes.DateTime)]
    public DateTime CreatedOn { get; set; }

    [FieldAttributes("OwnerId", "ownerid", FieldTypes.Lookup, "systemusers")]
    public Guid OwnerId { get; set; }
}
```

As you can see in the previous example, you define your class and use ```EntityAttributes``` class to set ```Entity logical name``` and ```Entity collection logical name```.

To configure each property is pretty simple, you only need to use the class ```FieldAttributes``` and set three or four values depending the field type in ```Dataverse```. The values for this attribute are:

- Property schema name.
- Property logical name.
- Property field type.
- Property linked entity collection logical name.

### Configure Dataverse service

Once that you have ready your configuration inside ```.json``` file and are created the ```entity classes``` you will need to call an extension method as you can see in the following code.

```
builder.Services.AddDataverseContext<DataverseContext>(builder =>
{
    builder.AddConnections(builder.Configuration.GetSection("Dataverses").Get<List<DataverseConnection>>()); // The default connection will be de first one.
    builder.SetDefaultConnection(
        builder.Configuration.GetSection("Dataverse").Get<DataverseConnection>()
    ); // You can set a default connection. If the connection does not exist, will add it.
    builder.AddEntityDeffinition<Employees>(); // You can add multiples entity deffinitions.
});
```

The last you need to do is use this library.

### Dataverse service example

In your controller you need to use ```IDataverseContext``` service.

```
public class EmployeesController : ControllerBase
{
    private readonly IDataverseContext _dataverse;

    public ContactsController(IDataverseContext dataverse)
        => _dataverse = dataverse;

    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _context.Set<Employees>().ToListAsync();
        return Ok(employees);
    }

    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var employee = await _context.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.Id, id)).FirstOrDefaultAsync();
        return Ok(employee);
    }

    public async Task<IActionResult> CreateEmployee(Employee model)
    {
        await _context.Set<Employees>().AddAsync(model);
        if(model.Id != Guid.Empty)
            return BadRequest();
        return Ok(model);
    }
    
    public async Task<IActionResult> UpdateEmployee(Guid id, Employee model)
    {
        var employee = await _context.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.Id, id)).FirstOrDefaultAsync();
        if(employee is null)
            return NotFound();
        
        model.Id = id;

        await _context.Set<Employees>().UpdateAsync(model);
        return Ok(model);
    }

    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _context.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.Id, id)).FirstOrDefaultAsync();
        if(employee is null)
            return NotFound();
        await _context.Set<Employees>().DeleteAsync(employee);
        return Ok();
    }
}

```

This is a simple example about the implementation of this library. If any error during the request execution ocurred, automatically will throw an exception. 

To get more information about the exception you can access to the data named ```Content``` of the exception generated by this library.

```
try 
{

}
catch(Exception ex)
{
    var content = ex.Data["Content"];
}
```

If you configured many connection, you can change between those using a code as the following.

```
public async Task<IActionResult> GetEmployee(Guid id)
{
    var employee = await _context.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.Id, id)).FirstOrDefaultAsync();
    if(employee is null) 
    {
        _context.SetEnvironment($"Name, unique identifier or dataverse connection instance");
        employee = await _context.Set<Employees>().FilterAnd(conditions => conditions.Equal(x => x.Id, id)).FirstOrDefaultAsync();
    }
    return Ok(employee);
}
```

Now you have all what you need to start to use ```Dataverse HTTP Connector Core```.

Remember that this library is an Open-Source project, and you can support it if you want.

## Repository

- [Dataverse.Http.Connector.Core](https://github.com/emmanuel-toledo/dataverse-http-connector-core)

## Authors

- [@emmanueltoledo](https://github.com/emmanuel-toledo)

