namespace Storage.Infrastructure.Services;

public sealed class TopicService:ApiService<Topic,TopicDto>
{
    private readonly IApiRepository<Topic> _rep;
    private readonly IMapper _mapper;
    private readonly ILogger<TopicService> _logger;
    private readonly string _modelType;

    public TopicService(
        IApiRepository<Topic> rep,
        IMapper mapper,
        ILogger<TopicService> logger,
        IValidator<TopicDto> validator)
        : base(rep, mapper, logger, validator)
    {
        _rep= rep;
        _mapper= mapper;
        _logger=logger;
        _modelType=_rep.GetModelType();
    }

    public override async Task<IEnumerable<TopicDto>> GetAsync()
    {
        var models = await _rep.GetAllByAsync()
            ?? throw new ModelNotFoundException($"'{_modelType}' collection not found");

        _logger.LogInformation("'{ModelType}' collection loaded successfully", _modelType);
        var dtos = _mapper.Map<IEnumerable<TopicDto>>(models).OrderBy(_ => _.Name);
        return dtos;
    }
}
