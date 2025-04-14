using BlogApp.Web.Context;
using BlogApp.Web.Extensions;
using BlogApp.Web.Mapping;
using BlogApp.Web.Repositories;
using BlogApp.Web.Repositories.CategoryRepositories;
using BlogApp.Web.Repositories.CommentRepositories;
using BlogApp.Web.Repositories.PostRepositories;
using BlogApp.Web.Seed;
using BlogApp.Web.Services.CategoryServices;
using BlogApp.Web.Services.CommentServices;
using BlogApp.Web.Services.PostServices;
using BlogApp.Web.Services.UserServices;
using BlogApp.Web.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFluentValidationExtension();

builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentityExtension();

builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddHttpContextAccessor();

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

await SeedData.AddDataAsync(app);

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Home/Error");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "post_details",
    pattern: "post/detail/{url}",
    defaults: new { controller = "Post", action = "Detail", }
);

app.MapControllerRoute(
    name: "post_category",
    pattern: "post/category/{categoryUrl}",
    defaults: new { controller = "Post", action = "Category", }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
