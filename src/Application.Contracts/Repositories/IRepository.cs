namespace Book.Application.Contracts.Repositories
{
    public interface IRepository<TClass>
    {
        public Task<bool> CreateAsync(TClass input);
        public Task<bool> UpdateAsync(int id, TClass input);
        public Task<bool> DeleteAsync(int id);
        public Task<TClass> GetAsync(int id);
        public Task<List<TClass>> GetListAsync();
    }
}
