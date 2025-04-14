using BlogApp.Web.Models.PostViewModels;
using FluentValidation;

namespace BlogApp.Web.Validations.PostValidations
{
    public class CreatePostViewModelValidator : AbstractValidator<CreatePostViewModel>
    {
        public CreatePostViewModelValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(20)
                .WithMessage("Başlık en fazla 20 karakter olmalıdır!")
                .NotNull()
                .WithMessage("Lütfen başlık giriniz!");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Lütfen açıklama giriniz!");

            RuleFor(x => x.Content)
                .NotNull()
                .WithMessage("Lütfen içerik giriniz!");
        }
    }
}
