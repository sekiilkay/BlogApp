using BlogApp.Web.Models.CommentViewModels;

namespace BlogApp.Web.Services.CommentServices
{
    public interface ICommentService
    {
        // Yorum Ekle
        Task CreateAsync(CreateCommentViewModel model);
    }
}
