
/* `var builder = WebApplication.CreateBuilder(args);` is creating a new instance of the
`WebApplication` class using the `CreateBuilder` method. This method initializes a new
`WebApplicationBuilder` object with the provided command-line arguments (`args`). The
`WebApplicationBuilder` is used to configure and build the web application. */
using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* `builder.Services.AddControllers();` is adding the controllers to the dependency injection
container. This allows the controllers to be resolved and instantiated by the framework when
handling incoming HTTP requests. Controllers are responsible for processing requests, executing
business logic, and returning responses. Adding controllers to the container enables the framework
to automatically handle the routing and execution of controller actions. */
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

/* The code `builder.Services.AddEndpointsApiExplorer();` and `builder.Services.AddSwaggerGen();` are
adding Swagger/OpenAPI support to the application. */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<StoreContext>(opt=>{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt=>{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
});

/* `app.UseHttpsRedirection();` is a middleware that redirects HTTP requests to HTTPS. It ensures that
all incoming requests are redirected to the secure HTTPS protocol. This helps to enforce secure
communication between the client and the server by automatically redirecting any insecure HTTP
requests to the corresponding HTTPS endpoint. */
// app.UseHttpsRedirection(); 

app.UseAuthorization();

/* `app.MapControllers();` is a middleware that maps the incoming HTTP requests to the appropriate
controller actions based on the routing configuration. It enables the framework to handle the
routing and execution of controller actions. */
app.MapControllers();


var scope=app.Services.CreateScope();
var context=scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger=scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    logger.LogError(ex,"A Problem occurs during migration");
}

/* `app.Run();` is the final step in configuring the HTTP request pipeline. It is responsible for
handling the incoming HTTP requests and generating the appropriate responses. When `app.Run();` is
called, the application starts listening for incoming requests and executes the middleware pipeline
to process those requests. The pipeline consists of a series of middleware components that are
executed in the order they are added, and each middleware component can modify the request or
response as needed. */
app.Run();
