namespace Storage.Api.Controllers;

[Route("topic")]
[ApiController]
[Produces("application/json")]
public class TopicController : ControllerBase
{
    private readonly IApiService<Topic, TopicDto> _service;

    public TopicController(IApiService<Topic, TopicDto> service)
    {
        _service= service;
    }

    /// <summary>
    /// Get all of <see cref="TopicDto"/> collection.
    /// </summary>
    /// <returns>
    /// <see cref="TopicDto"/> collection.
    /// </returns>
    /// <response code="200">Returns the <see cref="TopicDto"/> collection</response>
    /// <response code="204">If <see cref="TopicDto"/> collection is empty.</response>
    /// <response code="404">If <see cref="TopicDto"/> collection is null.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<TopicDto>>> Get()
    {
        var dtos=await _service.GetAsync();
        if (!dtos.Any()) return NoContent();
        return Ok(dtos);
    }

    /// <summary>Get <see cref="TopicDto"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <returns><see cref="TopicDto"/> instance.</returns>
    /// <response code="200">Returns the <see cref="TopicDto"/> instance.</response>
    /// <response code="404">If <see cref="TopicDto"/> instance is null.</response> 
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TopicDto>> Get(Guid id)
    {
        var dto=await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>Create a new instance of <see cref="TopicDto"/>.</summary>
    /// <param name="dto"></param>
    /// <returns>Created <see cref="TopicDto"/> instance.</returns>
    /// <response code="201">Returns the created <see cref="TopicDto"/> instance.</response>
    /// <response code="403">If <see cref="TopicDto"/> instance creating is failed </response>
    /// <response code="422">If <see cref="TopicDto"/> instance is not valid.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post([FromBody] TopicDto  dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id=result.Id},result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<ActionResult> Put(Guid id, [FromBody] TopicDto dto)
    {
        var result = await _service.UpdateAsync(id,dto);
        return AcceptedAtAction(nameof(Get),result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
