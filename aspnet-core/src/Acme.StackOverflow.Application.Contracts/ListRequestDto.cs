﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow
{
    public class ListRequestDto:PagedResultRequestDto
    {
        public ListRequestDto() {
            MaxResultCount = 1000;
        }
    }
}
