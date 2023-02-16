namespace Storage.Infrastructure.Validation;

public sealed class AuthorDtoValid:AbstractValidator<AuthorDto>
{
    public AuthorDtoValid()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}
