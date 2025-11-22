using EventScheduler.Data;
using EventScheduler.Services;
using EventSchedulerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Dependency Injection for services
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// CORS for React App
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS must be placed BEFORE Authorization/MVC
app.UseCors("AllowReactApp");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();