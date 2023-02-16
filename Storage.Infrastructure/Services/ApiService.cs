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

        if (validationResult.IsValid)
        {
            var model = _mapper.Map<T>(dto);
            var modelResult = await _rep.CreateAsync(model);            
            var dtoResult = _mapper.Map<D>(modelResult);
            _logger.LogInformation("'{ModelType}' created successfully", _modelType);
            return dtoResult;
        }

        else
        {
            _logger.LogError("Invalid value for '{ModelType}'", _modelType);
            throw new InvalidValueException($"Invalid value for '{_modelType}'");
        }
        
    }
    public async Task<D> UpdateAsync(D dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (validationResult.IsValid)
        {
            var model = _mapper.Map<T>(dto);
            var modelResult=await _rep.UpdateAsync(model);
            var dtoResult = _mapper.Map<D>(modelResult);
            _logger.LogInformation("'{ModelType}' modified successfully", _modelType);
            return dtoResult;
        }

        else
        {
            _logger.LogError("Invalid value for '{ModelType}'", _modelType);
            throw new InvalidValueException($"Invalid value for '{_modelType}'");
        }
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await _rep.GetOneByAsync(_ => _.Id.Equals(id))
            ??throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

         var result= await _rep.DeleteAsync(model);
        if (result)
        {
            _logger.LogInformation("'{ModelType}' with Id '{ModelId}' removed successfully", _modelType, id);
            return result;
        }

        else
        {
            _logger.LogError("'{ModelType}' with Id '{Id}' not deleted", _modelType,id);
            throw new InvalidRemovalException($"'{_modelType}' with Id '{id}' not deleted");
        } 
    }
}
