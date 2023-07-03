using GestorEstoque.Areas.Identity.Data;
using GestorEstoque.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestorEstoque
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("GestorEstoqueContextConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<GestorEstoqueContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<GestorEstoqueUsuario>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GestorEstoqueContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using(var scope = app.Services.CreateScope()){
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new [] { "Admin", "Funcionario" };

                foreach(var role in roles){
                    if(!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using(var scope = app.Services.CreateScope()){
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<GestorEstoqueUsuario>>();

                //Não se deve colocar senha escrita aqui. O correto é colocá-la numa variável de ambiente
                string emailAdm = "admin@admin.com";
                string senhaAdm = "123456Abc!";

                string emailFunc = "user@user.com";
                string senhaFunc = "123456Abc!";

                if(await userManager.FindByEmailAsync(emailAdm) == null){
                    var userAdm = new GestorEstoqueUsuario();
                    userAdm.UserName = emailAdm;
                    userAdm.Email = emailAdm;

                    await userManager.CreateAsync(userAdm, senhaAdm);
                    await userManager.AddToRoleAsync(userAdm, "Admin");
                }

                if(await userManager.FindByEmailAsync(emailFunc) == null){
                    var userFunc = new GestorEstoqueUsuario();
                    userFunc.UserName = emailFunc;
                    userFunc.Email = emailFunc;

                    await userManager.CreateAsync(userFunc, senhaFunc);
                    await userManager.AddToRoleAsync(userFunc, "Funcionario");
                }
            }

            app.Run();
        }
    }
}