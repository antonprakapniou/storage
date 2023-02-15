namespace Storage.Infrastructure.Services;

public class ApiService<T,D>:IApiService<T,D>
    where T : BaseModel
    where D : BaseDto
{
    private readonly IApiRepository<T> _repository;
    private readonly IMapper _mapper;

    public ApiService(IApiRepository<T> repository, IMapper mapper)
    {
        _repository=repository;
        _mapper=mapper;
    }

    public async Task<IEnumerable<D>> GetAsync()
    {
        var models = await _repository.GetAllByAsync()
            ??throw new ModelNotFoundException($"'{_repository.GetModelType()}' collection not fount");

        var dtos = _mapper.Map<IEnumerable<D>>(models);
        return dtos;
    }
    public async Task<D> GetByIdAsync(Guid id)
    {
        var model = await _repository.GetOneByAsync(_=>_.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_repository.GetModelType()}' with Id '{id}' not fount");

        var dto=_mapper.Map<D>(model);
        return dto;
    }
    public async Task CreateAsync(D dto)
    {
        var model = _mapper.Map<T>(dto);
        await _repository.CreateAsync(model);
    }
    public async Task UpdateAsync(D dto)
    {
        var model = _mapper.Map<T>(dto);
        await _repository.UpdateAsync(model);
    }
    public async Task DeleteAsync(Guid id)
    {
        var model = await _repository.GetOneByAsync(_ => _.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_repository.GetModelType()}' with Id '{id}' not fount");

        await _repository.DeleteAsync(model);
    }
}
