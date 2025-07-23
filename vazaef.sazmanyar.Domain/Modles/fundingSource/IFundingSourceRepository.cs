using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Domain.Modles.fundingSource
{
    public interface IFundingSourceRepository
    {
        Task<IEnumerable<FundingSource>> GetAllAsync();
        Task<FundingSource> GetByIdAsync(long id);
        Task AddAsync(FundingSource fundingSource);
        Task UpdateAsync(FundingSource fundingSource);
        Task DeleteAsync(long id);
        
    }
}
