using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using WebApiSample.Config;
using WebApiSample.Data;
using WebApiSample.Models;
using WebApiSample.Helpers;

namespace WebApiSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase("BookStore"));
            builder.Services.AddScoped<IRepo<Book>, DataRepository<Book>>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.ConfigureNlogLogger();
            builder.Services.AddMemoryCache();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample api",
                    Version = "v1",
                    Description = "jan 2022 sample crud api",
                    Contact = new OpenApiContact
                    {
                        Name = "students january",
                        Email = "some@email.com",
                        Url = new Uri("https://coolpage.com")
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opts.IncludeXmlComments(xmlPath);
            });
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}/nlog.config");



            var app = builder.Build();
            app.ConfigureExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample api V1"));
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", () => "Hello world");

            app.Run();
        }
    }
}