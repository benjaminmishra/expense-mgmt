using Microsoft.AspNetCore.Builder;
using ExpenseMgmt.Data;
using Microsoft.EntityFrameworkCore;

namespace Web;

public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddRazorPages();
        services.AddControllersWithViews();
        services.AddDbContext<ExpenseMgmtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ExpenseMgmtDb")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}
