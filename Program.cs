
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace breadhinge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Use Angular static files
            builder.Services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "wwwroot/breadhinge/dist/breadhinge/browser";
            });
  

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Dev
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(app.Environment.ContentRootPath, "wwwroot/breadhinge");

                if (app.Environment.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });

            // Use static files for prod
            if (!app.Environment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
