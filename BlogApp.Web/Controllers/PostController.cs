using BlogApp.Web.Extensions;
using BlogApp.Web.Models.CommentViewModels;
using BlogApp.Web.Models.PostViewModels;
using BlogApp.Web.Services.CategoryServices;
using BlogApp.Web.Services.CommentServices;
using BlogApp.Web.Services.PostServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        public PostController(IPostService postService, ICommentService commentService, ICategoryService categoryService)
        {
            _postService = postService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        // Post Listesi
        public IActionResult Index()
        {
            return View();
        }

        // Kategorideki Postlar
        public IActionResult Category(string categoryUrl)
        {
            ViewBag.categoryUrl = categoryUrl;
            return View();
        }

        // Post Detayı
        public async Task<IActionResult> Detail(string url)
        {
            var result = await _postService.GetByUrlPostAsync(url);
            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(CreateCommentViewModel model)
        {
            await _commentService.CreateAsync(model);

            return Json(new
            {
                username = model.UserName,
                text = model.Text,
                publishedDate = model.PublishedDate,
                avatar = model.Avatar
            });
        }

        // Kullanıcının Postları
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _postService.GetUserPostsAsync();
            return View(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            List<SelectListItem> values3 = (from x in categories
                                            select new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.CategoryId.ToString()
                                            }).ToList();
            ViewBag.v = values3;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            var result = await _postService.CreateAsync(model);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }

            return RedirectToAction("List", "Post");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeleteAsync(id);
            return RedirectToAction("List", "Post");
        }

        [Authorize]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _postService.ChangeStatusAsync(id);
            return RedirectToAction("List", "Post");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            var categories = await _categoryService.GetAllCategoriesAsync();
            List<SelectListItem> values3 = (from x in categories
                                            select new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.CategoryId.ToString()
                                            }).ToList();
            ViewBag.v = values3;

            var updatePost = new UpdatePostViewModel
            {
                IsActive = post.IsActive,
                CategoryId = post.CategoryId,
                Content = post.Content,
                Description = post.Description,
                Title = post.Title,
                PostId = post.PostId,
            };

            return View(updatePost);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostViewModel model)
        {
            var result = await _postService.UpdateAsync(model);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }

            return RedirectToAction("List", "Post");
        }
    }
}
