namespace Storage.Infrastructure.Interfaces;

public interface IApiService<T,D> 
    where T : BaseModel
    where D : BaseDto
{
    public Task<IEnumerable<D>> GetAsync();
    public Task<D> GetByIdAsync(Guid id);
    public Task CreateAsync(D dto);
    public Task UpdateAsync(D dto);
    public Task DeleteAsync(Guid id);
}
