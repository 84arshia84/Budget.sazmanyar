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
    }
}
