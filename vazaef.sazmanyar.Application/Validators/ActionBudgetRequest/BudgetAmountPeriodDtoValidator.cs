using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Validators.ActionBudgetRequest
{
    public class BudgetAmountPeriodDtoValidator
    {
        public void Validate(BudgetAmountPeriodDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // الگو برای اعداد با کاما یا نقطه (مثلاً 1,200 یا 1200.50)
            var numberPattern = @"^\d{1,3}(,\d{3})*(\.\d+)?$|^\d+(\.\d+)?$";

            #region اعتبارسنجی EstimationRange (سال+ماه مثلاً 140403)

            if (string.IsNullOrWhiteSpace(dto.EstimationRange))
                throw new ArgumentException("بازه برآورد نمی‌تواند خالی باشد.");

            if (!Regex.IsMatch(dto.EstimationRange, @"^\d{6}$"))
                throw new ArgumentException("بازه برآورد باید دقیقاً 6 رقم باشد (مثلاً 140403).");

            var yearPart = int.Parse(dto.EstimationRange.Substring(0, 4));
            var monthPart = int.Parse(dto.EstimationRange.Substring(4, 2));

            if (yearPart < 1300 || yearPart > 1500)
                throw new ArgumentException("سال در بازه برآورد باید بین 1300 تا 1500 باشد.");

            if (monthPart < 1 || monthPart > 12)
                throw new ArgumentException("ماه در بازه برآورد باید بین 01 تا 12 باشد.");

            #endregion

            #region اعتبارسنجی RequestedAmount

            if (string.IsNullOrWhiteSpace(dto.RequestedAmount))
                throw new ArgumentException("مقدار درخواستی نمی‌تواند خالی باشد.");

            if (!Regex.IsMatch(dto.RequestedAmount, numberPattern))
                throw new ArgumentException("مقدار درخواستی باید فقط عدد، یا عدد با کاما یا نقطه باشد.");

            #endregion

            #region اعتبارسنجی PlannedAmount

            if (string.IsNullOrWhiteSpace(dto.PlannedAmount))
                throw new ArgumentException("مقدار برنامه‌ریزی‌شده نمی‌تواند خالی باشد.");

            if (!Regex.IsMatch(dto.PlannedAmount, numberPattern))
                throw new ArgumentException("مقدار برنامه‌ریزی‌شده باید فقط عدد، یا عدد با کاما یا نقطه باشد.");

            #endregion
        }
    }
}