using FluentValidation;
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
    public class BudgetAmountPeriodDtoValidator
    {
        public void Validate(BudgetAmountPeriodDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var numberPattern = @"^\d{1,3}(,\d{3})*(\.\d+)?$|^\d+(\.\d+)?$"; // تطابق با عدد، کاما یا ممیز

            if (string.IsNullOrWhiteSpace(dto.EstimationRange) ||
                !Regex.IsMatch(dto.EstimationRange, numberPattern))
            {
                throw new ArgumentException("بازه برآورد باید فقط عدد یا عدد با کاما باشد.");
            }

            if (string.IsNullOrWhiteSpace(dto.RequestedAmount) ||
                !Regex.IsMatch(dto.RequestedAmount, numberPattern))
            {
                throw new ArgumentException("مقدار درخواستی باید فقط عدد یا عدد با کاما باشد.");
            }

            if (string.IsNullOrWhiteSpace(dto.PlannedAmount) ||
                !Regex.IsMatch(dto.PlannedAmount, numberPattern))
            {
                throw new ArgumentException("مقدار برنامه‌ریزی‌شده باید فقط عدد یا عدد با کاما باشد.");
            }
        }
    }

}

