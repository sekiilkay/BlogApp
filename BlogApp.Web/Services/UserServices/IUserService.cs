using BlogApp.Web.Entities;
using BlogApp.Web.Models;
using BlogApp.Web.Models.UserViewModels;

namespace BlogApp.Web.Services.UserServices
{
    public interface IUserService
    {
        Task<ResponseViewModel> RegisterAsync(RegisterViewModel registerViewModel);
        Task<ResponseViewModel> SignInAsync(SignInViewModel signInViewModel);
        Task LogOutAsync();
    }
}
