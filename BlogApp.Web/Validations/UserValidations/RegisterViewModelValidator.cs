using BlogApp.Web.Models.UserViewModels;
using FluentValidation;

namespace BlogApp.Web.Validations.UserValidations
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Lütfen adınızı giriniz!");

            RuleFor(x => x.Surname)
                .NotNull()
                .WithMessage("Lütfen soyadınızı giriniz!");

            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("Lütfen kullanıcı adınızı giriniz!");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Lütfen email adresinizi giriniz!")
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir email adresi giriniz!");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Lütfen şifrenizi giriniz!")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$") // Regular Expressions
                .WithMessage("Şifreniz en az 8 karakter uzunluğunda olmalı, bir büyük harf, bir küçük harf ve bir rakam içermelidir.");

            RuleFor(x => x.PasswordConfirm)
                .NotNull()
                .WithMessage("Lütfen şifrenizin aynısnı tekrar giriniz!")
                .Equal(x => x.Password)
                .WithMessage("Şifre onayı, şifre ile uyuşmalıdır.");
        }
    }
}
