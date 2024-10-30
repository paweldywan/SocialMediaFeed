using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaFeed.BLL;
using SocialMediaFeed.BLL.Interfaces;
using SocialMediaFeed.BLL.Services;
using SocialMediaFeed.DAL;

namespace SocialMediaFeed.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            var configuration = builder.Configuration;

            var environment = builder.Environment;

            AddServices(services, configuration, environment);

            var app = builder.Build();

            SeedDatabase(app);

            AddMiddlewares(app);

            app.MapControllers();

            app.MapRazorPages();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<SocialMediaFeedContext>();

            services.AddDataProtection()
                .PersistKeysToDbContext<SocialMediaFeedContext>();

            services.AddAuthorization();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (!context.Request.Path.StartsWithSegments("/Identity"))
                    {
                        context.Response.StatusCode = 403;
                    }
                    else
                    {
                        context.Response.Redirect(options.AccessDeniedPath);
                    }

                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToLogin = context =>
                {
                    if (!context.Request.Path.StartsWithSegments("/Identity"))
                    {
                        context.Response.StatusCode = 401;
                    }
                    else
                    {
                        context.Response.Redirect(options.LoginPath);
                    }

                    return Task.CompletedTask;
                };
            });
        }

        private static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<SocialMediaFeedSeeder>();

            seeder.Seed().Wait();
        }

        private static void ConfigureDatabase(IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddNpgsql<SocialMediaFeedContext>(connectionString);

            var isDevelopment = environment.IsDevelopment();

            if (isDevelopment)
                services.AddDatabaseDeveloperPageExceptionFilter();
        }

        private static void AddServices(IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(SocialMediaFeedMappingProfile).Assembly);

            ConfigureDatabase(services, configuration, environment);

            ConfigureIdentity(services);

            services.AddScoped<SocialMediaFeedSeeder>();

            services.AddScoped<IPostService, PostService>();
        }

        private static void AddMiddlewares(WebApplication app)
        {
            app.UseDefaultFiles();

            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();

                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
        }
    }
}
