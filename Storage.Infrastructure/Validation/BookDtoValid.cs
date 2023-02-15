namespace Storage.Infrastructure.Validation;

public sealed class BookDtoValid:AbstractValidator<BookDto>
{
	public BookDtoValid()
	{
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(HttpStatusCode.UnprocessableEntity.ToString())
            .WithMessage("required");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(HttpStatusCode.UnprocessableEntity.ToString())
            .WithMessage("required");

        RuleFor(x => x.TopicId)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(HttpStatusCode.UnprocessableEntity.ToString())
            .WithMessage("required");
    }
}
