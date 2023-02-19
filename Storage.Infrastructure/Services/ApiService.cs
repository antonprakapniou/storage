namespace Storage.Infrastructure.Services;

public class ApiService<T,D>:IApiService<T,D>
    where T : BaseModel
    where D : BaseDto
{
    private readonly IApiRepository<T> _rep;
    private readonly IMapper _mapper;
    private readonly ILogger<ApiService<T,D>> _logger;
    private readonly IValidator<D> _validator;
    private readonly string _modelType;

    public ApiService(
        IApiRepository<T> rep,
        IMapper mapper,
        ILogger<ApiService<T, D>> logger,
        IValidator<D> validator)
    {
        _rep=rep;
        _mapper=mapper;
        _logger=logger;
        _modelType=_rep.GetModelType();
        _validator=validator;
    }

    public virtual async Task<IEnumerable<D>> GetAsync()
    {
        var models = await _rep.GetAllByAsync()
            ?? throw new ModelNotFoundException($"'{_modelType}' collection not found");

        _logger.LogInformation("'{ModelType}' collection loaded successfully",_modelType);
        var dtos = _mapper.Map<IEnumerable<D>>(models);
        return dtos;
    }
    public virtual async Task<D> GetByIdAsync(Guid id)
    {
        var model = await _rep.GetOneByAsync(_=>_.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        _logger.LogInformation("'{ModelType}' with Id '{ModelId}' loaded successfully",_modelType,id);
        var dto =_mapper.Map<D>(model);
        return dto;
    }
    public async Task<D> CreateAsync(D dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid) 
            throw new InvalidValueException($"Invalid value for '{_modelType}'");

        var model = _mapper.Map<T>(dto);
        var modelResult = await _rep.CreateAsync(model)
            ?? throw new InvalidOperationException($"'{_modelType}' creating failed");

        _logger.LogInformation("'{ModelType}' with Id '{ModelId}' created successfully", _modelType,modelResult!.Id);
        var dtoResult = _mapper.Map<D>(modelResult);
        return dtoResult;
    }
    public async Task<D> UpdateAsync(Guid id,D dto)
    {
        if (!dto.Id.Equals(id))
            throw new InvalidOperationException($"'{_modelType}' Id '{dto.Id}' does not match Id '{id}' from request");

        if (!await _rep.IsExists(_=>_.Id.Equals(id)))
            throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid) 
            throw new InvalidValueException($"Invalid value for '{_modelType}'");

        var model = _mapper.Map<T>(dto);
        var modelResult = await _rep.UpdateAsync(model)
            ?? throw new InvalidOperationException($"'{_modelType}' updating failed");

        var dtoResult = _mapper.Map<D>(modelResult);
        _logger.LogInformation("'{ModelType}' modified successfully", _modelType);
        return dtoResult;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        Expression<Func<T, bool>> expression = _ => _.Id.Equals(id);

        if (!await _rep.IsExists(expression))
            throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        var result= await _rep.DeleteByAsync(expression);

        if (!result) throw new InvalidOperationException($"'{_modelType}' with Id '{id}' removing failed");

        _logger.LogInformation("'{ModelType}' with Id '{ModelId}' removed successfully", _modelType, id);
        return result;
    }
}
