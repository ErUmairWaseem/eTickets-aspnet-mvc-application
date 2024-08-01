using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DbContext configuration
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));



            //Services Configuration
            builder.Services.AddScoped<IActorsService, ActorsService>();
            
            builder.Services.AddScoped<IProducersService, ProducersService>();
            
            builder.Services.AddScoped<ICinemasService, CinemasService>();

            builder.Services.AddScoped<IMoviesService, MoviesService>();



            // Add services to the container.
            builder.Services.AddControllersWithViews();
             
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");
           
            AppDbInitializer.Seed(app);
            app.Run();

            
        }
    }
}