namespace Storage.Infrastructure.Interfaces;

public interface IApiService<T,D> 
    where T : BaseModel
    where D : BaseDto
{
    public Task<IEnumerable<D>> GetAsync();
    public Task<D> GetByIdAsync(Guid id);
    public Task<D> CreateAsync(D dto);
    public Task<D> UpdateAsync(D dto);
    public Task<bool> DeleteAsync(Guid id);
}
