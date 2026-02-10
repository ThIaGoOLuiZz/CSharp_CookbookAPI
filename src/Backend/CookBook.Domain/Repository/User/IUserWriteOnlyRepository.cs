namespace CookBook.Domain.Repository.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entities.User user);
    }
}
