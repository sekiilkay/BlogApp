using BlogApp.Web.Context;
using BlogApp.Web.Entities;

namespace BlogApp.Web.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }
    }
}
