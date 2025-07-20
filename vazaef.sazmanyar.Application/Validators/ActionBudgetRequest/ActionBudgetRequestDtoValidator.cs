using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Validators.ActionBudgetRequest
{
    public class ActionBudgetRequestDtoValidator 
    {
        public void Validate(ActionBudgetRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("عنوان بودجه نمی‌تواند خالی باشد.");

            
            if (dto.BudgetAmountPeriod == null || dto.BudgetAmountPeriod.Count == 0)
                throw new ArgumentException("حداقل یک بازه بودجه باید وارد شود.");

            var periodValidator = new BudgetAmountPeriodDtoValidator();

            foreach (var period in dto.BudgetAmountPeriod)
            {
                periodValidator.Validate(period);
            }
        }
    }
}
