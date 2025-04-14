using BlogApp.Web.Entities;
using BlogApp.Web.Models;
using BlogApp.Web.Models.PostViewModels;

namespace BlogApp.Web.Services.PostServices
{
    public interface IPostService
    {
        // Post ve Kategorisini Listele
        Task<List<ResultPostViewModel>> GetAllPostWithCategoryAsync();
        
        // Id'ye Göre Post Bul
        Task<GetByIdPostViewModel> GetPostByIdAsync(int id);
        
        // Url'e Göre Post Bul
        Task<GetByUrlPostDetailViewModel> GetByUrlPostAsync(string url);
        Task<ResponseViewModel> CreateAsync(CreatePostViewModel model);
        Task<ResponseViewModel> UpdateAsync(UpdatePostViewModel model);
        Task DeleteAsync(int id);
        
        // Post Durumunu Değiştir
        Task ChangeStatusAsync(int id);
        
        // Kullanıcnın Postlarını Listele
        Task<List<ResultPostViewModel>> GetUserPostsAsync();
        
        // Kategori'deki Postlar
        Task<List<ResultPostViewModel>> GetPostsByCategoryAsync(string categoryUrl);
    }
}
