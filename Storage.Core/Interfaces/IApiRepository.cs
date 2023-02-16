namespace Storage.Core.Interfaces;

public interface IApiRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>>? expression = null);
    public Task<T> GetOneByAsync(Expression<Func<T, bool>> expression);
    public Task <T> CreateAsync(T model);
    public Task <T> UpdateAsync(T model);
    public Task <bool> DeleteAsync(T model);
    public string GetModelType();
}
