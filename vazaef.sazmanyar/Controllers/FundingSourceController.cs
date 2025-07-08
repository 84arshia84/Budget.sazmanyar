using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Application.Services;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingSourceController : ControllerBase
    {
        private readonly IFundingSourceService _service;

        public FundingSourceController(IFundingSourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundingSource>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FundingSource>> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFundingSource(FundingSourceDto dto)
        {
            var fundingSource = new FundingSource
            {
                Description = dto.Description
            };

            await _service.AddAsync(fundingSource);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EditFundingSourceDto dto)
        {
            var EditFundingSourceDto = await _service.GetByIdAsync(id);
            if (EditFundingSourceDto == null)
                return NotFound();

            EditFundingSourceDto.Description = dto.Description;

            await _service.UpdateAsync(EditFundingSourceDto);
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
