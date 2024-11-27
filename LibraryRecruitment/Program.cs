using Library.Repositories;
using Library.Repositories.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<IContext, Context>();
builder.Services.AddTransient<IBooksRepository, BooksRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseMiddleware<LogMiddleware>();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAll");

app.UseCors(policy => policy
.SetIsOriginAllowed((_) => true)
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();

return;

public class LogMiddleware
{
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the request path
        Console.WriteLine($"Received request: {context.Request.Path}");
        
        // Call the next middleware in the pipeline
        await _next(context);
    }
}