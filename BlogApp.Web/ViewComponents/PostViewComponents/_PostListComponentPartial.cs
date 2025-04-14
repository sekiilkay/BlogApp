using BlogApp.Web.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewComponents.PostViewComponents
{
    public class _PostListComponentPartial(IPostService postService) : ViewComponent
    {
        // Postun Kategori ile Listelenmesi
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await postService.GetAllPostWithCategoryAsync();
            return View(result);
        }
    }
}
