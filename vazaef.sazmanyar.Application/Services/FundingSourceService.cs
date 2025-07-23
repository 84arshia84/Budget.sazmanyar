using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Services
{
    public class FundingSourceService : IFundingSourceService
    {
        private readonly IFundingSourceRepository _fundingSourceRepository;

        public FundingSourceService(IFundingSourceRepository fundingSourceRepository)
        {
            _fundingSourceRepository = fundingSourceRepository;
        }

        public async Task<IEnumerable<GetAllFundingSourceDto>> GetAllAsync()
        {
            var sources = await _fundingSourceRepository.GetAllAsync();
            return sources.Select(s => new GetAllFundingSourceDto
            {
                Id = s.Id,
                Description = s.Description
            });
        }

        public async Task<GetByIdFundingSourceDto> GetByIdAsync(long id)
        {
            var source = await _fundingSourceRepository.GetByIdAsync(id);
            if (source == null)
                return null;

            return new GetByIdFundingSourceDto
            {
                Id = source.Id,
                Description = source.Description
            };
        }

        public async Task AddAsync(AddFundingSourceDto dto)
        {
            var source = new FundingSource
            {
                Description = dto.Description
            };

            await _fundingSourceRepository.AddAsync(source);
        }

        public async Task UpdateAsync(long id, UpdateFundingSourceDto dto)
        {
            var existing = await _fundingSourceRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"RequestType {id} not found.");
            existing.Description = dto.Description;
            await _fundingSourceRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(long id)
        {
            await _fundingSourceRepository.DeleteAsync(id);
        }
    }
}