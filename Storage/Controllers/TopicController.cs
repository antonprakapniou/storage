namespace Storage.Api.Controllers;

/// <summary>
/// CRUD operations for <see cref="Topic"/>
/// </summary>
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
    /// Get all of <see cref="Topic"/> collection.
    /// </summary>
    /// <returns>
    /// <see cref="TopicDto"/> collection.
    /// </returns>
    /// <response code="200">Returns the <see cref="Topic"/> collection</response>
    /// <response code="204">If <see cref="Topic"/> collection is empty.</response>
    /// <response code="404">If <see cref="Topic"/> collection not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TopicDto>>> Get()
    {
        var dtos=await _service.GetAsync();
        if (!dtos.Any()) return NoContent();
        return Ok(dtos);
    }

    /// <summary>Get <see cref="Topic"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <returns><see cref="Topic"/> instance.</returns>
    /// <response code="200">Returns the <see cref="Topic"/> collection</response>
    /// <response code="404">If <see cref="Topic"/> instance not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TopicDto>> Get(Guid id)
    {
        var dto=await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>Create a new instance of <see cref="TopicDto"/>.</summary>
    /// <param name="dto"></param>
    /// <returns>Created <see cref="Topic"/> instance.</returns>
    /// <response code="201">Returns the created <see cref="Topic"/> instance.</response>
    /// <response code="400">If <see cref="Topic"/> instance creating is failed </response>
    /// <response code="422">If <see cref="Topic"/> instance is not valid.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] TopicDto  dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id=result.Id},result);
    }

    /// <summary>Modify an instance of <see cref="Topic"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>Modified <see cref="Topic"/> instance.</returns>
    /// <response code="202">Returns the modified <see cref="Topic"/> instance.</response>
    /// <response code="400">If <see cref="Topic"/> instance modifying is failed </response>
    /// <response code="422">If <see cref="Topic"/> instance is not valid.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] TopicDto dto)
    {
        var result = await _service.UpdateAsync(id,dto);
        return AcceptedAtAction(nameof(Get),new { id=result.Id}, result);
    }

    /// <summary>Remove an instance of <see cref="Topic"/> by <paramref name="id"/> identifier.</summary>
    /// <param name="id"></param>
    /// <response code="204">IF <see cref="Topic"/> instance removing is successfull.</response>
    /// <response code="400">If <see cref="Topic"/> instance removing is failed </response>
    /// <response code="404">If <see cref="Topic"/> instance not found.</response>
    /// <response code="500">If the generic error in the server.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
