using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IFundingSourceService
    {
        Task<IEnumerable<GetAllFundingSourceDto>> GetAllAsync();
        Task<GetByIdFundingSourceDto> GetByIdAsync(long id);
        Task AddAsync(AddFundingSourceDto dto);
        Task UpdateAsync(UpdateFundingSourceDto dto);
        Task DeleteAsync(long id);
    }
}
