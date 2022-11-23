namespace Book.Application.Contracts.Repositories
{
    public interface IRepository<TClass, TId>
    {
        public Task<bool> CreateAsync(TClass input);
        public Task<bool> UpdateAsync(TId id, TClass input);
        public Task<bool> DeleteAsync(TId id);
        public Task<TClass> GetAsync(TId id);
        public Task<List<TClass>> GetListAsync();
    }
}
