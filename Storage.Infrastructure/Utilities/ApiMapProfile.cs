namespace Storage.Infrastructure.Utilities;

public sealed class ApiMapProfile:Profile
{
    public ApiMapProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Topic, TopicDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
    }
}
