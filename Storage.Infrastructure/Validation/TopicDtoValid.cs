namespace Storage.Infrastructure.Validation;

public sealed class TopicDtoValid:AbstractValidator<TopicDto>
{
    public TopicDtoValid()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(HttpStatusCode.UnprocessableEntity.ToString())
            .WithMessage("required");
    }
}
