namespace Storage.Infrastructure.Services;

public class ApiService<T,D>:IApiService<T,D>
    where T : BaseModel
    where D : BaseDto
{
    private readonly IApiRepository<T> _rep;
    private readonly IMapper _mapper;
    private readonly ILogger<ApiService<T,D>> _logger;
    private readonly string _modelType;

    public ApiService(
        IApiRepository<T> rep,
        IMapper mapper,
        ILogger<ApiService<T, D>> logger)
    {
        _rep=rep;
        _mapper=mapper;
        _logger=logger;
        _modelType=_rep.GetModelType();
    }

    public virtual async Task<IEnumerable<D>> GetAsync()
    {
        var models = await _rep.GetAllByAsync()
            ?? throw new ModelNotFoundException($"'{_modelType}' collection not found");

        _logger.LogInformation($"'{_modelType}' collection loaded successfully");
        var dtos = _mapper.Map<IEnumerable<D>>(models);
        return dtos;
    }
    public virtual async Task<D> GetByIdAsync(Guid id)
    {
        var model = await _rep.GetOneByAsync(_=>_.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        _logger.LogInformation($"'{_modelType}' with Id '{id}' loaded successfully");
        var dto =_mapper.Map<D>(model);
        return dto;
    }
    public async Task CreateAsync(D dto)
    {
        var model = _mapper.Map<T>(dto);        
        await _rep.CreateAsync(model);
        _logger.LogInformation($"'{_modelType}' created successfully");
    }
    public async Task UpdateAsync(D dto)
    {
        var model = _mapper.Map<T>(dto);        
        await _rep.UpdateAsync(model);
        _logger.LogInformation($"'{_modelType}' with Id '{model.Id}' modified successfully");
    }
    public async Task DeleteAsync(Guid id)
    {
        var model = await _rep.GetOneByAsync(_ => _.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        await _rep.DeleteAsync(model);
        _logger.LogInformation($"'{_modelType}' with Id '{id}' removed successfully");
    }
}
