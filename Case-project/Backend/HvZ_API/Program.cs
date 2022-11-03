using HvZ_API.Contexts;
using HvZ_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace HvZ_API
{
    public class Program
    {

        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            var client = new HttpClient();
                            var keyUri = builder.Configuration.GetValue<string>("keycloak:keyUri");

                            var response = client.GetAsync(keyUri).Result;
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);

                            return keys.Keys;
                        },
                        ValidIssuers = new List<string>
                        {
                            builder.Configuration.GetValue<string>("keycloak:issuerUri")
                        },
                        ValidAudiences = new List<string> { "hvz-client", "localhost_client" }
                    };
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HvZ API",
                    Version = "api",
                    Description = "A HvZ Api for the HvZ application",
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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                };
                c.AddSecurityRequirement(securityRequirement);
            });



            builder.Services.AddDbContext<HvZDbEfContext>(
             opt => opt.UseSqlServer(
                                builder.Configuration.GetConnectionString("azure")
                //builder.Configuration.GetConnectionString("MovieDb")

                )
            );
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            //Add custom Services here based on this format:
            // builder.Services.AddTransient<ICharacterService, CharacterServiceImp>(); // Transient is the default behaviour and means a new instance is made when injected.

            builder.Services.AddTransient<IChatService, ChatServiceImp>();
            builder.Services.AddTransient<IPlayerService, PlayerServiceImp>();
            builder.Services.AddTransient<ISquadService, SquadServiceImp>();
            builder.Services.AddTransient<IMissionService, MissionsServiceImp>();
            builder.Services.AddTransient<IGameService, GameServiceImp>();
            builder.Services.AddTransient<IGameConfigService, GameConfigServiceImp>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
