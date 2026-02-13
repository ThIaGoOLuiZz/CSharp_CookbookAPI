namespace CookBook.Domain.Repository
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
