namespace Storage.Api.Controllers;

/// <summary>
/// CRUD operations for <see cref="Author"/>
/// </summary>
[Route("author")]
[ApiController]
[Produces("application/json")]
public class AuthorController : ControllerBase
{
    private readonly IApiService<Author, AuthorDto> _service;

    public AuthorController(IApiService<Author, AuthorDto> service)
    {
        _service= service;
    }

    /// <summary>
    /// Get all of <see cref="Author"/> collection.
    /// </summary>
    /// <returns>
    /// <see cref="Author"/> collection.
    /// </returns>
    /// <response code="200">Returns the <see cref="Author"/> collection</response>
    /// <response code="204">If <see cref="Author"/> collection is empty.</response>
    /// <response code="404">If <see cref="Author"/> collection not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
    {
        var dtos = await _service.GetAsync();
        if (!dtos.Any()) return NoContent();
        return Ok(dtos);
    }

    /// <summary>Get <see cref="Author"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <returns><see cref="Author"/> instance.</returns>
    /// <response code="200">Returns the <see cref="Author"/> collection</response>
    /// <response code="404">If <see cref="Author"/> instance not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> Get(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>Create a new instance of <see cref="Author"/>.</summary>
    /// <param name="dto"></param>
    /// <returns>Created <see cref="Author"/> instance.</returns>
    /// <response code="201">Returns the created <see cref="Author"/> instance.</response>
    /// <response code="400">If <see cref="Author"/> instance creating is failed </response>
    /// <response code="422">If <see cref="Author"/> instance is not valid.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AuthorDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Modify an instance of <see cref="Author"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>Modified <see cref="Author"/> instance.</returns>
    /// <response code="202">Returns the modified <see cref="Author"/> instance.</response>
    /// <response code="400">If <see cref="Author"/> instance modifying is failed </response>
    /// <response code="422">If <see cref="Author"/> instance is not valid.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AuthorDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Remove an instance of <see cref="Author"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <response code="204">IF <see cref="Author"/> instance removing is successfull.</response>
    /// <response code="400">If <see cref="Author"/> instance removing is failed </response>
    /// <response code="404">If <see cref="Author"/> instance not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
