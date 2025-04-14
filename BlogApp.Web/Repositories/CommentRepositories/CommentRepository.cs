using BlogApp.Web.Context;
using BlogApp.Web.Entities;

namespace BlogApp.Web.Repositories.CommentRepositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {
        }
    }
}
