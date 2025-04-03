using Core.Entites.Identity;
using Core.Interfaces;
using Library.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repo.Data;
using Repo.Data.IdentityContext;
using Repo.Data.Repos;
using StackExchange.Redis;
using Talabat.api.Attributes;
using Talabat.Core.ServiceContract;

namespace Library
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure DbContext for StoreContext and UserContext
            builder.Services.AddDbContext<StoreContext>(options =>
                options.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;TrustServerCertificate=True;"));

            builder.Services.AddDbContext<UserContext>(options =>
                options.UseSqlServer("Server=.;Database=UserContext;Trusted_Connection=True;TrustServerCertificate=True;"));


            // Caching
            builder.Services.AddSingleton<IConnectionMultiplexer>((serverprovider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            // Configure Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<UserContext>();

            // Register Generic Repository
            builder.Services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            ////Register Caching
            //builder.Services.AddScoped(typeof(ICache), typeof(CacheAttributes));

            var app = builder.Build();

            // Database Migration and Seeding
            #region Database Migration and Seeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<UserContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await _identityDbContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration or seeding.");
            }
            #endregion


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
