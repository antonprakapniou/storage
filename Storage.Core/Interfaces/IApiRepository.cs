namespace Storage.Core.Interfaces;

public interface IApiRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>>? expression = null);
    public Task<T> GetOneByAsync(Expression<Func<T, bool>> expression);
    public Task CreateAsync(T model);
    public Task UpdateAsync(T model);
    public Task DeleteAsync(T model);
    public string GetModelType();
}
