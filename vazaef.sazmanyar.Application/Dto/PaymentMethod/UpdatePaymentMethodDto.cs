﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.PaymentMethod
{
    public class UpdatePaymentMethodDto : CreatePaymentMethodDto
    {
        public long Id { get; set; }
    }
}
