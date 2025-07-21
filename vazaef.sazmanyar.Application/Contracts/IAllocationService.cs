using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Allocation;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IAllocationService
    {
        Task AddAsync(CreateAllocationDto dto);
    }
}
