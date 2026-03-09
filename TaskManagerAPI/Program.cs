using TaskManagerAPI.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("react",
        policy =>
        {
            policy.WithOrigins("http://localhost:64772")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<TaskSqlService>();
builder.Services.AddScoped<LogInSqlService>();

var app = builder.Build();

app.MapControllers();

app.UseCors("react");

app.MapGet("/", () => "Hello World!");

app.Run();