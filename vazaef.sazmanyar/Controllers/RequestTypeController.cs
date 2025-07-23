using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Application.Dto.RequestType;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTypeController : ControllerBase
    {
        private readonly IRequestTypeService _service;

        public RequestTypeController(IRequestTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllRequestTypeDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdRequestTypeDto>> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRequestTypeDto dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "نوع درخواست با موفقیت اضافه شد." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateRequestTypeDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
