using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestingDepartmentController : ControllerBase
    {
        private readonly IRequestingDepartmentService _service;

        public RequestingDepartmentController(IRequestingDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestingDepartment>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestingDepartment>> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRequestingDepartment(RequestingDepartmentDto dto)
        {
            var RequestingDepartment = new RequestingDepartment
            {
                Description = dto.Description
            };

            await _service.AddAsync(RequestingDepartment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EditRequestingDepartmentDto dto)
        {
            var existingDepartment = await _service.GetByIdAsync(id);
            if (existingDepartment == null)
                return NotFound();

            existingDepartment.Description = dto.Description;

            await _service.UpdateAsync(existingDepartment);
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

