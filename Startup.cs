using Microsoft.EntityFrameworkCore;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.Repository.Implementation;
using SecondAPIAssignmentRepo.Repository.Interface;

namespace SecondAPIAssignmentRepo
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddScoped<EFDataContext>();            
            services.AddDbContext<EFDataContext>(options =>
            options.UseNpgsql(configRoot.GetConnectionString("WebApiDatabase")));            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
