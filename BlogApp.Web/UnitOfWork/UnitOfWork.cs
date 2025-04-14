
using BlogApp.Web.Context;

namespace BlogApp.Web.UnitOfWork
{
    public class UnitOfWork(BlogContext context) : IUnitOfWork
    {
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
