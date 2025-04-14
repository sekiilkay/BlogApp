using BlogApp.Web.Context;
using BlogApp.Web.Entities;
using BlogApp.Web.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Repositories.PostRepositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context)
        {
        }

        public async Task<Post> ChangeStatusAsync(int id)
        {
            return (await _context.Posts
                .Where(x => x.PostId == id)
                .FirstOrDefaultAsync())!;
        }

        public async Task<List<Post>> GetAllPostWithCategoryAsync()
        {
            return await _context.Posts
                .Include(x => x.Category)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<Post> GetByUrlPostAsync(string url)
        {
            return (await _context.Posts
                .Include(x => x.Category)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.AppUser)
                .Where(x => x.Url == url)
                .FirstOrDefaultAsync())!;
        }

        public async Task<List<Post>> GetPostsByCategoryAsync(string categoryUrl)
        {
            var category = await _context.Categories
                .Where(x => x.Url == categoryUrl)
                .FirstOrDefaultAsync();

            return await _context.Posts
                .Where(x => x.Category == category && x.IsActive == true)
                .ToListAsync();
        }

        public async Task<List<Post>> GetUserPostsAsync(Guid userId)
        {
            return await _context.Posts
                .Include(x => x.Category)
                .Where(x => x.AppUserId == userId)
                .ToListAsync();
        }
    }
}
