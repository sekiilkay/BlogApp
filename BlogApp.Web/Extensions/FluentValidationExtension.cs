using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace BlogApp.Web.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationExtension(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
