namespace Storage.Api.Controllers;

[Route("topic")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly IApiService<Topic, TopicDto> _service;

    public TopicController(IApiService<Topic, TopicDto> service)
    {
        _service= service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<TopicDto>>> Get()
    {
        var dtos=await _service.GetAsync();
        if (!dtos.Any()) return NoContent();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TopicDto>> Get(Guid id)
    {
        var dto=await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post([FromBody] TopicDto  dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get),result);
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
        var result = await _service.DeleteAsync(id);
        return AcceptedAtAction(nameof(Get), result);
    }
}
