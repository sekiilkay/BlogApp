using BlogApp.Web.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewComponents.CategoryViewComponents
{
    public class _CategoryListComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _CategoryListComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return View(result);
        }
    }
}
