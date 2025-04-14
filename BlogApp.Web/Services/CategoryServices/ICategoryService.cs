using BlogApp.Web.Models.CategoryViewModels;

namespace BlogApp.Web.Services.CategoryServices
{
    public interface ICategoryService
    {
        // Kategori Listesi
        Task<List<ResultCategoryViewModel>> GetAllCategoriesAsync();
    }
}
