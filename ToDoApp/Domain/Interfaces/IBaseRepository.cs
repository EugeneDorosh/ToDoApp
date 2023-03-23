namespace Repository.Interfaces
{
    public interface IBaseRepository
    {
        public Task<bool> SaveAsync();
    }
}
