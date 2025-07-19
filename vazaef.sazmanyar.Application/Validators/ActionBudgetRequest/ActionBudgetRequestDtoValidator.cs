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
    public class ActionBudgetRequestDtoValidator : AbstractValidator<ActionBudgetRequestDto>
    {
        public void Validate(ActionBudgetRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("عنوان بودجه نمی‌تواند خالی باشد.");

            if (dto.TotalActionBudget <= 0)
                throw new ArgumentException("مقدار بودجه باید بیشتر از صفر باشد.");

            if (dto.BudgetAmountPeriod == null || dto.BudgetAmountPeriod.Count == 0)
                throw new ArgumentException("حداقل یک بازه بودجه باید وارد شود.");

            var numberPattern = @"^\d{1,3}(,\d{3})*(\.\d+)?$|^\d+(\.\d+)?$"; // تطابق با اعداد صحیح یا با کاما

            foreach (var period in dto.BudgetAmountPeriod)
            {
                if (string.IsNullOrWhiteSpace(period.EstimationRange) ||
                    !Regex.IsMatch(period.EstimationRange, numberPattern))
                {
                    throw new ArgumentException("بازه برآورد باید فقط عدد یا عدد با کاما باشد.");
                }

                if (string.IsNullOrWhiteSpace(period.RequestedAmount) ||
                    !Regex.IsMatch(period.RequestedAmount, numberPattern))
                {
                    throw new ArgumentException("مقدار درخواستی باید فقط عدد یا عدد با کاما باشد.");
                }

                if (string.IsNullOrWhiteSpace(period.PlannedAmount) ||
                    !Regex.IsMatch(period.PlannedAmount, numberPattern))
                {
                    throw new ArgumentException("مقدار برنامه‌ریزی‌شده باید فقط عدد یا عدد با کاما باشد.");
                }
            }
        }
    }
}
