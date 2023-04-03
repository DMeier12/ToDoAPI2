using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
    options =>
    {
        //The string in the parameter for GetConnectionString() should match the name in appsettings.json
        options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDB"));
    }
    );

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
     {
         policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
         policy.WithOrigins("OriginPolicy", "http://localhost:3001").AllowAnyMethod().AllowAnyHeader();
     });
});

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

    app.UseCors();
    app.Run();
