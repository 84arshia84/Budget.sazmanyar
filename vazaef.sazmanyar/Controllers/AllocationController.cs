using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Allocation;
using vazaef.sazmanyar.Application.Services;

namespace vazaef.sazmanyar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationService _allocationService;

        public AllocationController(IAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAllocationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _allocationService.AddAsync(dto);
            return Ok(new { message = "Allocation created successfully." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateAllocationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _allocationService.UpdateAsync(id, dto);
                return Ok(new { message = "Allocation updated successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "Allocation not found." });
            }
        }

        // DELETE: api/Allocation/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _allocationService.DeleteAsync(id);
            return Ok(new { message = "Allocation deleted successfully." });
        }

        // GET: api/Allocation/5
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var allocation = await _allocationService.GetByIdAsync(id);
            if (allocation == null)
                return NotFound(new { message = "Allocation not found." });

            return Ok(allocation);
        }

        // GET: api/Allocation
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _allocationService.GetAllAsync();
            return Ok(result);
        }

    }
}
