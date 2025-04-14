using BlogApp.Web.Models.UserViewModels;
using FluentValidation;

namespace BlogApp.Web.Validations.UserValidations
{
    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x => x.Email)
               .NotNull()
               .WithMessage("Lütfen email adresinizi giriniz!")
               .EmailAddress()
               .WithMessage("Lütfen geçerli bir email adresi giriniz!");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Lütfen şifrenizi giriniz!");
        }
    }
}
