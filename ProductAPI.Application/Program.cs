
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Configuration;
using ProductAPI.Infra;
using System.Text.Json.Serialization;

namespace ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:8080", "http://localhost:8081")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            DependencyInjection.Configure(builder.Services);
            ProductAPIContext.Configure(builder.Services, 
                builder.Configuration.GetConnectionString("DefaultConnection") ?? "", 
                builder.Environment.IsEnvironment("Testing"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
            {
                if(!app.Environment.IsEnvironment("Testing"))
                    app.ApplyMigrations();

                app.Urls.Add("http://*:80");
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
