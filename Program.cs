using Assignment.DTOs;
using Assignment.Models;
using Assignment.Repository;
using Assignment.Service;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Assignment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
             builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
             ));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5174","http://localhost:5173") // Frontend URL
                          .AllowAnyMethod()  // GET, POST, etc.
                          .AllowAnyHeader(); // Custom headers
                });
            });

            MapsterConfig.RegisterMappings();

            builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            builder.Services.AddScoped<IMapper, Mapper>();
            builder.Services.AddScoped<ICandidatesRepository, CandidatesRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<ICertificateRepository, CertificatesRepository>();
            builder.Services.AddScoped<IPhotoIdRepository, PhotoIdRepository>();
            builder.Services.AddScoped<ICandidatesAnalyticsRepository, CandidatesAnalyticsRepository>();

            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
             });

            builder.Services.AddIdentityCore<Candidate>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secret = builder.Configuration["JwtConfig:Secret"];
                var issuer = builder.Configuration["JwtConfig:ValidIssuer"];
                var audience = builder.Configuration["JwtConfig:ValidAudiences"];

                if (secret is null || issuer is null || audience is null)
                {
                    throw new ApplicationException("Jwt is not set in the configuration");
                }
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });

            builder.Services.AddAuthorization( options =>
            {
                    options.AddPolicy("RequireAdministratorRole", policy =>
                    policy.RequireRole(AppRoles.Administrator));
                    options.AddPolicy("RequireUserRole", policy =>
                    policy.RequireRole(AppRoles.User));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                // Ensure database is created and apply migrations
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.EnsureCreated();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(AppRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
                }
                if (!await roleManager.RoleExistsAsync(AppRoles.Administrator))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.Administrator));
                }
            }

            app.MapControllers();

            app.Run();
        }
    }
}
