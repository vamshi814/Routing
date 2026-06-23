var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//enabling the routing middleware for our application
app.UseRouting();

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
});
 
//if no URL matches the above endpoints, then this will be execute
app.Run(async (context) =>
{
    await context.Response.WriteAsync("No URL matched! Please check the URL");
});

//app.MapGet("/", () => "Hello World!");

app.Run();
