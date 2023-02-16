﻿namespace Storage.Infrastructure.Validation;

public sealed class BookDtoValid:AbstractValidator<BookDto>
{
	public BookDtoValid()
	{
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.AuthorId).NotEmpty().NotNull();
        RuleFor(x => x.TopicId).NotEmpty().NotNull();
    }
}
