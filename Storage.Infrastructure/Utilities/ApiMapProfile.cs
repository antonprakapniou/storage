namespace Storage.Infrastructure.Utilities;

public sealed class ApiMapProfile:Profile
{
    public ApiMapProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
    }
}
