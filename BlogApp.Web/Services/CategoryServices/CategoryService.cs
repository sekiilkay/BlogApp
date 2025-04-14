using AutoMapper;
using BlogApp.Web.Models.CategoryViewModels;
using BlogApp.Web.Repositories.CategoryRepositories;

namespace BlogApp.Web.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<ResultCategoryViewModel>>(categories);
        }
    }
}
