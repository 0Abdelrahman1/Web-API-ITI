
using Microsoft.EntityFrameworkCore;
using Project.Contexts;
using Project.Filters;
using Project.Middlewares;
using Project.Repositories;
using Serilog;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.

            //builder.Services.AddControllers(op => op.Filters.Add<HandleExceptionFilterAttribute>());
            builder.Services.AddControllers();
            
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            builder.Services.AddDbContext<StudentManagementDB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                //options.AddDefaultPolicy(builder =>
                //    builder.AllowAnyOrigin()
                //           .AllowAnyMethod()
                //           .AllowAnyHeader())

                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.WithOrigins("http://example.com")
                           .WithMethods("GET")
                           .WithHeaders("Authorization"));

            });
 
            var app = builder.Build();

            app.UseExceptionHandle();

            app.UseLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();
            //app.UseStaticFiles();
            app.UseCors("AllowAll");


            app.MapControllers();

            app.Run();
        }
    }
}
