using Assignment_3_backend_api.Models;
using Assignment_3_backend_api.Services.Characters;
using Assignment_3_backend_api.Services.Franchises;
using Assignment_3_backend_api.Services.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Assignment_3_backend_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movies API",
                    Version = "api",
                    Description = "An assignment of the Noroff Accelerate Fullstack course.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Noroff Accelerate",
                        Email = "utdanning@noroff.no",
                        Url = new Uri("https://www.noroff.no/accelerate"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<MovieDbEfContext>(
             opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("MovieDb")
                )
            );
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            // Custom Services
            builder.Services.AddTransient<ICharacterService, CharacterServiceImp>(); // Transient is the default behaviour and means a new instance is made when injected.
            builder.Services.AddTransient<IFranchiseService, FranchiseServiceImp>(); // Transient is the default behaviour and means a new instance is made when injected.
            builder.Services.AddTransient<IMovieService, MovieServiceImp>(); // Transient is the default behaviour and means a new instance is made when injected.



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

            app.Run();
        }
    }
}