using Azure.Core;
using BlogApp.Web.Entities;
using BlogApp.Web.Models;
using BlogApp.Web.Models.UserViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Web.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IValidator<RegisterViewModel> _registerValidator;
        private readonly IValidator<SignInViewModel> _signInValidator;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IValidator<RegisterViewModel> registerValidator, IValidator<SignInViewModel> signInValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerValidator = registerValidator;
            _signInValidator = signInValidator;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // Kayıt Ol 
        public async Task<ResponseViewModel> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerViewModel);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseViewModel.Fail(validationErrors);
            }

            var createUser = new AppUser
            {
                Email = registerViewModel.Email,
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                UserName = registerViewModel.UserName,
                ImageUrl = "anonim.png"
            };

            await _userManager.CreateAsync(createUser, registerViewModel.Password);

            return ResponseViewModel.Success();
        }

        // Giriş Yap
        public async Task<ResponseViewModel> SignInAsync(SignInViewModel signInViewModel)
        {
            var validationResult = await _signInValidator.ValidateAsync(signInViewModel);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseViewModel.Fail(validationErrors);
            }

            var hasUser = await _userManager.FindByEmailAsync(signInViewModel.Email);

            if (hasUser is null)
            {
                return ResponseViewModel.Fail("Email veya Şifre yanlış");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, signInViewModel.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return ResponseViewModel.Fail("Email veya Şifre yanlış");
            }

            return ResponseViewModel.Success();
        }
    }
}
