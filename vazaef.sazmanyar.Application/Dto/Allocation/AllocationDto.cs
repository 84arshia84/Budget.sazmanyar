using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.Allocation
{
    public class AllocationDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public long RequestId { get; set; }
        public List<ActionAllocationDto> ActionAllocations { get; set; } = new();
    }
}
