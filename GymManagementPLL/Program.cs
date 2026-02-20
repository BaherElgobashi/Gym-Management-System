using GymManagementDAL.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymManagementDAL.Data.DataSeed;
using GymManagementBLL;
using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.AttachmentService;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GymManagementBLL.Services.Interfaces;

namespace GymManagementPLL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Add services to make the dependency Injection for GymDbContext.
            builder.Services.AddDbContext<GymDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

                // Another way to define connection string.

                //Options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);

            });

            // Add services to make the dependency Injection for GenericRepository<>.
            //builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();

            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            builder.Services.AddScoped<ISessionRepository, SessionRepository>();

            builder.Services.AddScoped<IAnalyticsService , AnalyticsService> ();
            

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

            builder.Services.AddScoped<IMemberService, MemberService>();

            builder.Services.AddScoped<ITrainerService, TrainerService>();

            builder.Services.AddScoped<IPlanService, PlanService>();

            builder.Services.AddScoped<ISessionService , SessionService>();

            builder.Services.AddScoped<IAttachmentService , AttachmentService>();

            builder.Services.AddScoped<IAccountService , AccountService>();

            builder.Services.AddIdentity<ApplicationUser , IdentityRole>(Config =>
            {
                Config.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GymDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            //builder.Services.AddIdentityCore<ApplicationUser>()
            //                .AddEntityFrameworkStores<GymDbContext>();                   


            var app = builder.Build();

            // DataSeeding.

            #region Data Seeding - Migrate Database.

            using var Scope = app.Services.CreateScope();

            var dbContext = Scope.ServiceProvider.GetRequiredService<GymDbContext>();

            var PendingMigrations = dbContext.Database.GetPendingMigrations();

            if (PendingMigrations?.Any() ?? false) 
            {
                dbContext.Database.Migrate();
            }


            GymDbContextSeeding.SeedData(dbContext);


            //=======================================================================

            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            await IdentityDbContextSeeding.SeedDataAsync(roleManager, userManager);













            //var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            //if (PendingMigrations?.Any() ?? false)
            //    await dbContext.Database.MigrateAsync();

            //await GymContextSeeding.SeedDateAsync(dbContext);

            #endregion



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
