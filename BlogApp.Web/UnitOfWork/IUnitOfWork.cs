namespace BlogApp.Web.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
