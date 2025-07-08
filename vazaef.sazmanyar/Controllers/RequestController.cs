using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Request;

namespace vazaef.sazmanyar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class RequestController : ControllerBase
    {
        private readonly IRequestService _service;

        public RequestController(IRequestService service)
        {
            _service = service;
        }

        // GET: api/Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetAllRequests()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        // GET: api/Request/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetRequestById(long id)
        {
            var request = await _service.GetByIdAsync(id);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        // POST: api/Request
        [HttpPost]
        public async Task<IActionResult> CreateRequest(CreateRequestDto dto)
        {
            await _service.AddAsync(dto);
            return Ok();
        }

        // PUT: api/Request/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(long id, EditRequestDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result)    
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Request/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
