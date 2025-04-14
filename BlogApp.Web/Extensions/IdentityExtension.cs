using BlogApp.Web.Context;
using BlogApp.Web.Entities;

namespace BlogApp.Web.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<BlogContext>();

            // Cookie 
            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();

                cookieBuilder.Name = "BlogApp";
                opt.LoginPath = new PathString("/User/SignIn");
                opt.LogoutPath = new PathString("/User/LogOut");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromHours(2);
                opt.SlidingExpiration = true;
            });

            return services;
        }
    }
}
