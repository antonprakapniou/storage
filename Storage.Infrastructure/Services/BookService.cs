namespace Storage.Infrastructure.Services;

public sealed class BookService : ApiService<Book, BookDto>
{
    private readonly IApiRepository<Book> _bookRep;
    private readonly IApiRepository<Author> _authorRep;
    private readonly IApiRepository<Topic> _topicRep;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> _logger;
    private readonly string _modelType;

    public BookService(
        IApiRepository<Book> bookRep,
        IApiRepository<Author> authorRep,
        IApiRepository<Topic> topicRep,
        IMapper mapper,
        ILogger<BookService> logger,
        IValidator<BookDto> validator) 
        : base(bookRep, mapper, logger,validator)
    {
        _bookRep= bookRep;
        _authorRep= authorRep;
        _topicRep= topicRep;
        _mapper= mapper;
        _logger= logger;
        _modelType=_bookRep.GetModelType();
    }

    public override async Task<IEnumerable<BookDto>> GetAsync()
    {
        var models = await _bookRep.GetAllByAsync()
            ?? throw new ModelNotFoundException($"'{_modelType}' collection not found");

        _logger.LogInformation("'{ModelType}' collection loaded successfully", _modelType);
        var dtos = _mapper.Map<IEnumerable<BookDto>>(models).OrderBy(_=>_.Topic!.Name).ThenBy(_=>_.Name).ThenBy(_=>_.Author!.Name);
        foreach (var dto in dtos) await SetPropAsync(dto);
        return dtos;
    }
    public override async Task<BookDto> GetByIdAsync(Guid id)
    {
        if (!await _bookRep.IsContains(_=>_.Id.Equals(id))) throw new ModelNotFoundException($"'{_modelType}' with Id '{id}' not found");

        var model = await _bookRep.GetOneByAsync(_ => _.Id.Equals(id));
        _logger.LogInformation("'{ModelType}' with Id '{ModelId}' loaded successfully", _modelType, id);
        var dto = _mapper.Map<BookDto>(model);
        await SetPropAsync(dto);
        return dto;
    }
    private async Task SetPropAsync(BookDto bookDto)
    {
        var author = (bookDto.AuthorId is null) ? null : await _authorRep.GetOneByAsync(_ => _.Id.Equals(bookDto.AuthorId));
        var topic = (bookDto.TopicId is null) ? null : await _topicRep.GetOneByAsync(_ => _.Id.Equals(bookDto.TopicId));
        bookDto.Author=(author is null) ? null : _mapper.Map<AuthorDto>(author);
        bookDto.Topic=(topic is null) ? null : _mapper.Map<TopicDto>(topic);
    }
}
