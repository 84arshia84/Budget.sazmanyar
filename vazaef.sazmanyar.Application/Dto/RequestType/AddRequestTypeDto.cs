using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.RequestType
{
    public class AddRequestTypeDto 
    {
        [Required]
        public string Description { get; set; }
    }
}
