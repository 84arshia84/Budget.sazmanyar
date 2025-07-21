using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.PaymentMethod;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;

namespace vazaef.sazmanyar.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _repository;

        public PaymentMethodService(IPaymentMethodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PaymentMethodDto>> GetAllAsync()
        {
            var methods = await _repository.GetAllAsync();
            return methods.Select(m => new PaymentMethodDto { Id = m.Id, Name = m.Name }).ToList();
        }

        public async Task<PaymentMethodDto> GetByIdAsync(long id)
        {
            var method = await _repository.GetByIdAsync(id);
            return new PaymentMethodDto { Id = method.Id, Name = method.Name };
        }

        public async Task CreateAsync(CreatePaymentMethodDto dto)
        {
            var method = new PaymentMethod { Name = dto.Name };
            await _repository.AddAsync(method);
        }

        public async Task UpdateAsync(long id, CreatePaymentMethodDto dto)
        {
            var method = await _repository.GetByIdAsync(id);
            method.Name = dto.Name;
            await _repository.UpdateAsync(method);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
