﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.RequestingDepartmen
{
    public class AddRequestingDepartmenDto
    {
        [Required]
        public string Description { get; set; }
    }
}
