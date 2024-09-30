using breadhinge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using SpotifyAPI.Web;

namespace breadhinge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton(SpotifyClientConfig.CreateDefault());
            builder.Services.AddScoped<SpotifyClientBuilderService>();
            builder.Services.AddScoped<SpotifyService>();

            builder.Services.AddControllersWithViews();

            // Spotify auth
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Spotify", policy =>
                {
                    policy.AuthenticationSchemes.Add("Spotify");
                    policy.RequireAuthenticatedUser();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            }).AddSpotify(options =>
            {
                options.ClientId = builder.Configuration["SpotifyApi:ClientId"] ?? "";
                options.ClientSecret = builder.Configuration["SpotifyApi:ClientSecret"] ?? "";
                options.CallbackPath = "/Auth/callback";
                options.SaveTokens = true;

                var scopes = new List<string>
                {
                    Scopes.UserReadPrivate,
                    Scopes.UserLibraryModify,
                    Scopes.PlaylistModifyPrivate,
                    Scopes.PlaylistModifyPublic,
                    Scopes.UserLibraryRead
                };

                options.Scope.Add(string.Join(",", scopes));
            });

            builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/", "Spotify");
            });

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
