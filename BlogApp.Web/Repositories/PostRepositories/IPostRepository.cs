using BlogApp.Web.Entities;
using BlogApp.Web.Models.PostViewModels;

namespace BlogApp.Web.Repositories.PostRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        // Postları kategorisi ile birlikte listele
        Task<List<Post>> GetAllPostWithCategoryAsync();
        
        // Url'e göre post bul
        Task<Post> GetByUrlPostAsync(string url);
        
        // Kullanıcının postlarını listele
        Task<List<Post>> GetUserPostsAsync(Guid userId);
        
        // Post durumunu değiştir (aktif/pasif)
        Task<Post> ChangeStatusAsync(int id);

        // Kategorideki postları listele
        Task<List<Post>> GetPostsByCategoryAsync(string categoryUrl);
    }
}
