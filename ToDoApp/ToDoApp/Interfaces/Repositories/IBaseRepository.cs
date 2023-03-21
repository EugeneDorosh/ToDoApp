namespace ToDoApp.Interfaces
{
    public interface IBaseRepository
    {
        public Task<bool> SaveAsync();
    }
}
