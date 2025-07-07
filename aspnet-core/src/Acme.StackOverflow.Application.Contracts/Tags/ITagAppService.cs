using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.StackOverflow.Tags
{
    public interface ITagAppService :
        ICrudAppService< // ABP will generate all standard methods
            TagDto,
            Guid,
            ListRequestDto,
            CreateUpdateTagDto,
            CreateUpdateTagDto>
    {
    }
}
