namespace Storage.Api.Models;

public sealed class ErrorDetails
{
    public string? Message { get; set; }
    public string? StackTrace { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}
