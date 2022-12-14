namespace Book.Application.Contracts.Repositories
{
    public interface IRepository<T, TId>
    {
        public IQueryable<T> Entities { get; }
        public Task<T> GetAsync(TId id);
        public Task<List<T>> GetListAsync();
        public Task<T> CreateAsync(T input);
        public Task CreateManyAsync(IEnumerable<T> entities);
        public Task UpdateAsync(TId id, T input);
        public Task UpdateManyAsync(IEnumerable<T> entities);
        public Task DeleteAsync(TId id);
        public Task DeleteManyAsync(IEnumerable<T> entities);
    }
}
