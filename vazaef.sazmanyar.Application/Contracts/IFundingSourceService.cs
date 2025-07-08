using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IFundingSourceService
    {
        Task<IEnumerable<FundingSource>> GetAllAsync();
        Task<FundingSource> GetByIdAsync(long id);
        Task AddAsync(FundingSource fundingSource);
        Task UpdateAsync(FundingSource fundingSource);
        Task DeleteAsync(long id);
    }
}
