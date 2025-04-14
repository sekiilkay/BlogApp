using BlogApp.Web.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewComponents.PostViewComponents
{
    public class _PostListByCategoryComponentPartial(IPostService postService) : ViewComponent
    {
        // Kategorideki Postlar
        public async Task<IViewComponentResult> InvokeAsync(string categoryUrl)
        {
            var result = await postService.GetPostsByCategoryAsync(categoryUrl);
            return View(result);
        }
    }
}
