using Microsoft.EntityFrameworkCore;
using Samurai_V2_.Net_8.DbContexts;
using Samurai_V2_.Net_8.DependencyContainer;
using Samurai_V2_.Net_8.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<BookContexts>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
var DbHost=Environment.GetEnvironmentVariable("DB_HOST");
var DbName=Environment.GetEnvironmentVariable("DB_NAME");
var DbPassword=Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

if (DbHost != null)
    {
    
     var connectionString = $"Data Source ={DbHost}; Initial Catalog={DbName};User Id=sa;Password={DbPassword};Connection Timeout=30;";
     builder.Services.AddDbContext<BookContexts>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));
    }
    else
    {


        DependencyInversion.RegisterServices(builder.Services);
        builder.Services.AddCors(options =>
                        options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    }
var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My DEVELOPER V1");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My LIVE V1");
    });
}


app.UseHttpsRedirection();
app.UseCors("Open");
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
