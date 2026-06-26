var builder = WebApplication.CreateBuilder(args);

//step2 for custom route constraints
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("alphaNumeric", typeof(Routing.CustomConstraints.AlphaNumericConstraint));
});
var app = builder.Build();


//enabling the routing middleware for our application
app.UseRouting();

//tracking endpoint
app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync( "Typed URL: "+ endpoint.DisplayName);
        await context.Response.WriteAsync("\n");
    }
    await next();
});

//creating endpoints for our application
app.UseEndpoints( async endpoint =>
{
    endpoint.Map("/Home", async(HttpContext context)=>{
          await context.Response.WriteAsync("Your in Home! Page");
    });
    endpoint.MapGet("/Product", async (context) =>
    {
        context.Response.WriteAsync("Your in Product! Page");
    });
    endpoint.MapPost("/Product", async (context) =>
    {
        context.Response.WriteAsync("New Product! Created");
    });

    //Route Parameters : url segments captures values specidied at that position
    endpoint.MapGet("/Book/{id:int}", async (context) =>
    {
        var id = Convert.ToInt32(context.Request.RouteValues["id"]);
        context.Response.WriteAsync($"Your in Book! Page with id: {id}");
    });
    endpoint.MapGet("/Book/Author/{name?}", async (context) =>
    {
        //this is direct casting as string if missing name the name is null not "" empty string
        //var name = context.Request.RouteValues["name"] as string;
        var name = Convert.ToString(context.Request.RouteValues["name"]);
        if (!string.IsNullOrEmpty(name))
        {
            
            context.Response.WriteAsync($"Your in Book! Author name : {name}");
        }
        else
        {
            context.Response.WriteAsync($"Your at /Book/Author");
        }
        
    });
    //for custom route constraints
    endpoint.MapGet("/User/{username:alphaNumeric}", async (context) =>
    {
        var username = Convert.ToString(context.Request.RouteValues["username"]);
        context.Response.WriteAsync($"Your in User! Page with username: {username}");
    });

});
 
//if no URL matches the above endpoints, then this will be execute
app.Run(async (context) =>
{
    await context.Response.WriteAsync("No URL matched! Please check the URL");
});

//app.MapGet("/", () => "Hello World!");

app.Run();
