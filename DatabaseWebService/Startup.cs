using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server=DESKTOP-U9I41QJ/SQLEXPRESS;Database=Diplom;Trusted_Connection=True;";
            // устанавливаем контекст данных
            services.AddDbContext<DiplomContext>(options => options.UseSqlServer(con));

            services.AddRazorPages();
            services.AddControllers(); // используем контроллеры без представлений
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

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });

            app.Run();
        }
    }
}
