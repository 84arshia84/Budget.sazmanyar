using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Application.Services
{
    public class FundingSourceService : IFundingSourceService
    {
        private readonly IFundingSourceRepository _fundingSourceRepository;

        public FundingSourceService(IFundingSourceRepository fundingSourceRepository)
        {
            _fundingSourceRepository = fundingSourceRepository;
        }

        public async Task<IEnumerable<FundingSource>> GetAllAsync()
        {
            return await _fundingSourceRepository.GetAllAsync();
        }

        public async Task<FundingSource> GetByIdAsync(long id)
        {
            return await _fundingSourceRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(FundingSource fundingSource)
        {
            await _fundingSourceRepository.AddAsync(fundingSource);
        }

        public async Task UpdateAsync(FundingSource fundingSource)
        {
            await _fundingSourceRepository.UpdateAsync(fundingSource);
        }

        public async Task DeleteAsync(long id)
        {
            await _fundingSourceRepository.DeleteAsync(id);
        }
    }
}
