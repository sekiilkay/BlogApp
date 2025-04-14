using BlogApp.Web.Models.UserViewModels;
using BlogApp.Web.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    
    public class UserController(IUserService userService) : Controller
    {  /*Primary constructor*/
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var result = await userService.RegisterAsync(registerViewModel);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View();

            }

            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            var result = await userService.SignInAsync(signInViewModel);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View();

            }

            return RedirectToAction("Index","Post");
        }
        public async Task<IActionResult> LogOut()
        {
            await userService.LogOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
