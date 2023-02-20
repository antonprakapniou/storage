namespace Storage.Infrastructure.Services;

public sealed class AuthorService : ApiService<Author, AuthorDto>
{
    private readonly IApiRepository<Author> _rep;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorService> _logger;
    private readonly string _modelType;

    public AuthorService(
        IApiRepository<Author> rep,
        IMapper mapper,
        ILogger<AuthorService> logger,
        IValidator<AuthorDto> validator)
        : base(rep, mapper, logger,validator)
    {
        _rep= rep;
        _mapper= mapper;
        _logger=logger;
        _modelType=_rep.GetModelType();
    }

    public override async Task<IEnumerable<AuthorDto>> GetAsync()
    {
        var models = await _rep.GetAllByAsync() ?? throw new ModelNotFoundException($"'{_modelType}' collection not found");
        _logger.LogInformation("'{ModelType}' collection loaded successfully", _modelType);
        var dtos = _mapper.Map<IEnumerable<AuthorDto>>(models).OrderBy(_ => _.Name);
        return dtos;
    }
}
