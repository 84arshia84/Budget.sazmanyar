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

        public RequestTypeController(IRequestTypeService requestTypeService)
        {
            _service = requestTypeService;
        }

        // GET: api/RequestType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestType>>> GetRequestTypes()
        {
            var requestTypes = await _service.GetAllRequestTypesAsync();
            return Ok(requestTypes);
        }

        // GET: api/RequestType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestType>> GetRequestType(long id)
        {
            var requestType = await _service.GetRequestTypeByIdAsync(id);

            if (requestType == null)
            {
                return NotFound();
            }

            return Ok(requestType);
        }

        // POST: api/RequestType
        [HttpPost]
        public async Task<ActionResult> CreateRequestType(RequestTypeDto dto)
        {
            var RequestType = new RequestType
            {
                Description = dto.Description
            };

            await _service.AddAsync(RequestType);
            return Ok();
        }

        // PUT: api/RequestType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EditRequestingDepartmentDto dto)
        {
            var EditRequestingDepartmentDto = await _service.GetRequestTypeByIdAsync(id);
            if (EditRequestingDepartmentDto == null)
                return NotFound();

            EditRequestingDepartmentDto.Description = dto.Description;

            await _service.UpdateAsync(EditRequestingDepartmentDto);
            return NoContent();
        }

        // DELETE: api/RequestType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRequestType(long id)
        {
            await _service.DeleteRequestTypeAsync(id);
            return NoContent();
        }
    }
}
